using HotelManagementSystem.Common;
using HotelManagementSystem.DomainServices;

namespace HotelManagementSystem.Aggregates;

public class Booking : Aggregate<BookingId>
{
    public ReservationDates ReservationDates { get; private set; } = default!;
    public NumberOfGuests NumberOfGuests { get; private set; } = default!;
    public HotelId HotelId  { get; private set; } = default!;
    public RoomId RoomId  { get; private set; } = default!;
    public Discount Discount  { get; private set; } = default!;
    public BookingPrice BookingPrice  { get; private set; } = default!;
    
    
    public static Booking Create(BookingId id, ReservationDates reservationDates, NumberOfGuests numberOfGuests, 
        HotelId hotelId, RoomId roomId, Discount discount, BookingPrice bookingPrice, bool isDeleted = false)
    {
        var booking = new Booking
        {
            Id = id,
            ReservationDates = reservationDates,
            NumberOfGuests = numberOfGuests,
            HotelId = hotelId,
            RoomId = roomId,
            Discount = discount,
            BookingPrice = bookingPrice
        };
        return booking;
    }
    
    private static bool IsRoomAvailable(Room room, ReservationDates reservationDates, List<Booking> bookings)
    {
        foreach (var booking in bookings)
        {
            var isBookingsOverlap = reservationDates.IsOverlapping(booking.ReservationDates.ArrivalDate, booking.ReservationDates.DepartureDate);
            var isCurrentBookingContained = reservationDates.IsContained(booking.ReservationDates.ArrivalDate, booking.ReservationDates.DepartureDate);
            var isRoomAlreadyBooked = booking.RoomId == room.Id;

            if ((isBookingsOverlap || isCurrentBookingContained) && isRoomAlreadyBooked)
            {
                return false;
            }
        }

        return true;
    }
    
    public static async Task<Booking> AddBooking(List<Hotel> listHotels, List<Booking> bookings, DateTime arrivalDate, DateTime departureDate, int numberOfGuests)
    {
        var reservationDates = new ReservationDates(arrivalDate, departureDate);

        listHotels.Shuffle();

        foreach (var hotel in listHotels)
        {
            var rooms = hotel.Rooms
                .Where(r => r.Capacity == numberOfGuests)
                .ToList();

            if (rooms.Any())
            {
                rooms.Shuffle();

                var isAnyRoomAvailable = false;
                foreach (var room in rooms)
                {
                    var isRoomAvailable = true;
                    foreach (var booking in bookings)
                    {
                        var isBookingsOverlap = reservationDates.IsOverlapping(booking.ReservationDates.ArrivalDate, booking.ReservationDates.DepartureDate);
                        var isCurrentBookingContained = reservationDates.IsContained(booking.ReservationDates.ArrivalDate, booking.ReservationDates.DepartureDate);
                        var isRoomAlreadyBooked = booking.RoomId == room.Id;

                        if ((isBookingsOverlap || isCurrentBookingContained) && isRoomAlreadyBooked)
                        {
                            isRoomAvailable = false;
                        }
                    }

                    if (isRoomAvailable)
                    {
                        isAnyRoomAvailable = true;

                        double hotelStarRating = hotel.HotelStarRating;
                        decimal basePrice = room.RoomPrice;
                        decimal discount = (decimal)(hotelStarRating * 5.0);
                        decimal bookingPrice = basePrice * (discount / 100);

                        var booking = Booking.Create(BookingId.Of(Guid.NewGuid()), ReservationDates.Of(arrivalDate, departureDate),
                            NumberOfGuests.Of(numberOfGuests), HotelId.Of(hotel.Id), RoomId.Of(room.Id), Discount.Of(discount), BookingPrice.Of(bookingPrice));
                        return booking;
                    }
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