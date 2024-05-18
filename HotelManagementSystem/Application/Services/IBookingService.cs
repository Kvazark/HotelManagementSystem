using HotelManagementSystem.Aggregates;
using HotelManagementSystem.DTO;

namespace HotelManagementSystem.Application.Services;

public interface IBookingService
{
    public Task<Booking> AddBooking(DateTimeOffset arrivalDate, DateTimeOffset departureDate, int numberOfGuests);
    
    public Task<BookingDto?> GetBookingById(Guid bookingId);

    public Task<List<HotelBookingStatsDto>> GetBookingStatsByHotel();
}