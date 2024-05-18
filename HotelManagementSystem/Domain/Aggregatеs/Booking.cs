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
            var rooms = listRooms.Where(r => r.Hotel.Id == hotel.Id && r.Capacity == numberOfGuests).ToList();
            
            if (rooms.Any())
            {
                var isAnyRoomAvailable = false;
                
                var selectRoom = new SelectRoomCommand(rooms, bookings, reservationDates, hotel);
                var selectRoomResult = await mediator.Send(selectRoom);
                if (selectRoomResult.room != null)
                {
                    isAnyRoomAvailable = true;

                    var applyDiscount = new ApplyDiscountCommand(hotel.HotelStarRating, selectRoomResult.room.RoomPrice);
                    var applyDiscountResult = await mediator.Send(applyDiscount);
                    
                    var booking = Booking.Create(BookingId.Of(Guid.NewGuid()), ReservationDates.Of(arrivalDate, departureDate),
                        NumberOfGuests.Of(numberOfGuests), hotel, selectRoomResult.room, Discount.Of(applyDiscountResult.discount), BookingPrice.Of(applyDiscountResult.bookingPrice));
                    
                    return booking;
                }
                if (!isAnyRoomAvailable)
                {
                    continue;
                }
            }
        }
        return null;
    }

}