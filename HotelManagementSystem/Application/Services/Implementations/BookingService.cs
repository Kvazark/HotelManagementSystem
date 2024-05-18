using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Application.Services;
using HotelManagementSystem.Data;
using HotelManagementSystem.Domain.Events;
using HotelManagementSystem.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.DomainServices;

public class BookingService : IBookingService
{
    private readonly HotelBookingContext _context;
    private readonly IMediator _mediator;

    public BookingService(HotelBookingContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Booking> AddBooking(DateTimeOffset arrivalDate, DateTimeOffset departureDate, int numberOgGuests)
    {
        var hotels = await _context.Hotels
            .ToListAsync();
        var rooms = await _context.Rooms
            .ToListAsync();
        
        var today = DateTime.UtcNow.Date;
        if (departureDate > today && arrivalDate > today)
        {
            var bookings = _context.Bookings
                .Where(b => b.ReservationDates.DepartureDate > today)
                .ToList();
            var newBooking = Booking.AddBooking(_mediator, hotels, rooms, bookings, arrivalDate.UtcDateTime, departureDate.UtcDateTime, numberOgGuests);
            if (newBooking != null)
            {
                _context.Bookings.Add(await newBooking);
                await _context.SaveChangesAsync();
                var bookingCreatedEvent = new BookingCreateDomainEvent(await newBooking);
                await _mediator.Publish(bookingCreatedEvent);
            }

            return await newBooking;
        }
        return null;
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
    
    public async Task<List<HotelBookingStatsDto>> GetBookingStatsByHotel()
    {
        var bookings = _context.Bookings
            .Include(b => b.Hotel)
            .Include(b => b.Room)
            .ToList();

        var hotelBookingStats = bookings
            .GroupBy(b => b.Hotel)
            .Select(g => new HotelBookingStatsDto
            {
                Hotel = g.Key.Name,
                TotalBookings = g.Count(),
                RoomBookingStats = g.GroupBy(b => b.Room.NumberRoom)
                    .Select(r => new RoomBookingStats
                    {
                        RoomNumber = r.Key,
                        BookingCount = r.Count(),
                        TotalGuests = r.Sum(b => b.NumberOfGuests)
                    })
                    .ToList(),
                TotalGuests = g.Sum(b => b.NumberOfGuests),
            })
            .ToList();

        return hotelBookingStats;
    }
}