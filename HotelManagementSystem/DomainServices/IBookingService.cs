using HotelManagementSystem.Aggregates;
using HotelManagementSystem.DTO;
using Microsoft.VisualBasic;

namespace HotelManagementSystem.DomainServices;

public interface IBookingService
{
    public Task<Booking> ToBook(CreateBookingDto createBookingDto);
    
    public Task<BookingDto?> GetBookingById(string bookingId);
}