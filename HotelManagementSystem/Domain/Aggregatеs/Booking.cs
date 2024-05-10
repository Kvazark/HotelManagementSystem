using HotelManagementSystem.Application.Features;
using HotelManagementSystem.Domain.Aggregatеs;
using HotelManagementSystem.Domain.Common;
using MediatR;

namespace HotelManagementSystem.Aggregates;

public class Booking 
{
    public Guid Id { get; private set; } = default!;
    public ReservationDates ReservationDates { get; private set; } = default!;
    public NumberOfGuests NumberOfGuests { get; private set; } = default!;
    public Hotel Hotel  { get; private set; } = default!;
    public Room Room { get; private set; } = default!;
    public Discount Discount  { get; private set; } = default!;
    public BookingPrice BookingPrice  { get; private set; } = default!;

    public Booking()
    {
        
    }
    public static Booking Create(BookingId id, ReservationDates reservationDates, NumberOfGuests numberOfGuests, 
        Hotel hotel, Room room, Discount discount, BookingPrice bookingPrice, bool isDeleted = false)
    {
        var booking = new Booking
        {
            Id = id,
            ReservationDates = reservationDates,
            NumberOfGuests = numberOfGuests,
            Hotel = hotel,
            Room = room,
            Discount = discount,
            BookingPrice = bookingPrice
        };
        return booking;
    }
    

    public static async Task<Booking> AddBooking(IMediator mediator,List<Hotel> listHotels,List<Room> listRooms, List<Booking> bookings, DateTime arrivalDate, DateTime departureDate, int numberOfGuests)
    {
        var reservationDates = new ReservationDates(arrivalDate, departureDate);

        listHotels.Shuffle();

        foreach (var hotel in listHotels)
        {
            var rooms = listRooms.Where(r => r.Hotel.Id == hotel.Id).ToList();
            
            if (rooms.Any())
            {
                var isAnyRoomAvailable = false;
                
                var command = new SelectRoomCommand(rooms, bookings, reservationDates);
                var selectRoomResult = await mediator.Send(command);
                if (selectRoomResult.room != null)
                {
                    isAnyRoomAvailable = true;
                    
                    double hotelStarRating = hotel.HotelStarRating;
                    decimal basePrice = selectRoomResult.room.RoomPrice; 
                    decimal discount = (decimal)(hotelStarRating * 5.0);
                    decimal bookingPrice = basePrice * (discount / 100);
                    
                    var booking = Booking.Create(BookingId.Of(Guid.NewGuid()), ReservationDates.Of(arrivalDate, departureDate),
                        NumberOfGuests.Of(numberOfGuests), hotel, selectRoomResult.room, Discount.Of(discount), BookingPrice.Of(bookingPrice));
                    
                    return booking;
                }
                if (isAnyRoomAvailable)
                {
                    continue;
                }
            }
        }
        return null;
    }

}