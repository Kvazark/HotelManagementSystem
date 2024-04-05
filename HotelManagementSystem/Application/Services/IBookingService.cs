using HotelManagementSystem.Aggregates;
using HotelManagementSystem.DTO;
using Microsoft.VisualBasic;

namespace HotelManagementSystem.DomainServices;

public interface IBookingService
{
    public Task<Booking> AddBooking(DateTime createBookingDto, DateTime departureDate, int numberOfGuests);
    
    public Task<BookingDto?> GetBookingById(string bookingId);
}