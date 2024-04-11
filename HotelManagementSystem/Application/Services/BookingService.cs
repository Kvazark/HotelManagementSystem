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

    public async Task<Booking> AddBooking(DateTime arrivalDate, DateTime departureDate, int numberOgGuests)
    {
        var hotels = await _context.Hotels
            .Include(h => h.Rooms)
            .ToListAsync();
        
        var today = DateTime.Today;
        var bookings = _context.Bookings
            .Where(b => b.ReservationDates.DepartureDate > today)
            .ToList();
        var newBooking = Booking.AddBooking(hotels, bookings, arrivalDate, departureDate, numberOgGuests);
        if (newBooking != null)
        {
            _context.Bookings.Add(await newBooking);
            await _context.SaveChangesAsync();
        }
        return await newBooking;
    }

    public async Task<Booking> GetBookingById(Guid bookingId)
    {
        var booking = _context.Bookings
            .FirstOrDefault(b => b.Id == bookingId);
        return booking;
    }
}