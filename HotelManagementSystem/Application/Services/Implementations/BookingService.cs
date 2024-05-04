using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Data;
using HotelManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.DomainServices;

public class BookingService : IBookingService
{
    private readonly HotelBookingContext _context;

    public BookingService(HotelBookingContext context)
    {
        _context = context;
    }

    public async Task<Booking> AddBooking(DateTimeOffset arrivalDate, DateTimeOffset departureDate, int numberOgGuests)
    {
        var hotels = await _context.Hotels
            .ToListAsync();
        var rooms = await _context.Rooms
            .ToListAsync();
        
        var today = DateTime.UtcNow.Date;
        var bookings = _context.Bookings
            .Where(b => b.ReservationDates.DepartureDate > today)
            .ToList();
        var newBooking = Booking.AddBooking(hotels, rooms, bookings, arrivalDate.UtcDateTime, departureDate.UtcDateTime, numberOgGuests);
        if (newBooking != null)
        {
            _context.Bookings.Add(await newBooking);    
            await _context.SaveChangesAsync();
        }
        return await newBooking;
    }

    public async Task<BookingDto?> GetBookingById(Guid bookingId)
    {
        var booking = await _context.Bookings
            .Where(b => b.Id == bookingId)
            .Include(b => b.Hotel)
            .Include(b => b.Room)
            .Include(b => b.ReservationDates)
            .AsNoTracking()
            .Select(b => new BookingDto
            {
                ReservationDates = new ReservationDatesDto
                {
                    ArrivalDate = b.ReservationDates.ArrivalDate,
                    DepartureDate = b.ReservationDates.DepartureDate
                },
                NumberOfGuests = b.NumberOfGuests,
                Discount = b.Discount,
                FinalPrice = b.BookingPrice,
                NameHotel = b.Hotel.Name,
                Address = b.Hotel.Address,
                HotelStarRating = b.Hotel.HotelStarRating,
                NumberOfRoom = b.Room.NumberRoom,
                CategoryRoom = b.Room.RoomCategory
            })
            .FirstOrDefaultAsync();
        return booking;
    }
}