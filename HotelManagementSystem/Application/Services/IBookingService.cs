using HotelManagementSystem.Aggregates;
using HotelManagementSystem.DTO;
using Microsoft.VisualBasic;

namespace HotelManagementSystem.DomainServices;

public interface IBookingService
{
    public Task<Booking> AddBooking(DateTimeOffset createBookingDto, DateTimeOffset departureDate, int numberOfGuests);
    
    public Task<BookingDto?> GetBookingById(Guid bookingId);
}