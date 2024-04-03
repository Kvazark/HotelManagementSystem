using HotelManagementSystem.Common;

namespace HotelManagementSystem.Aggregates;

public class Booking : Aggregate<BookingId>
{
    public ReservationDates ReservationDates { get; private set; } = default!;
    public NumberOfGuests NumberOfGuests { get; private set; } = default!;
    public Hotel Hotel  { get; private set; } = default!;
    public Room Room  { get; private set; } = default!;
    public Discount Discount  { get; private set; } = default!;
    public BookingPrice BookingPrice  { get; private set; } = default!;
    
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

}