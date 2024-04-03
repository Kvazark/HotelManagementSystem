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
    
    public async Task<Booking> ToBook(CreateBookingDto createBookingDto)
    {
        var reservationDates = new ReservationDates(createBookingDto.ArrivalDate, createBookingDto.DepartureDate);
        var numberofGuests = new NumberOfGuests(createBookingDto.NumberOfGuests);
       
        var hotels = await _context.Hotels.ToListAsync();

        // Перемешивание порядка отелей случайным образом
        hotels.Shuffle();
        
        foreach (var hotel in hotels)
        {
            var rooms = await _context.Rooms
                .Where(r => r.HotelId == hotel.Id && r.Capacity == createBookingDto.NumberOfGuests)
                .ToListAsync();

            if (rooms.Any())
            {
                // Перемешивание порядка комнат случайным образом
                rooms.Shuffle();

                // Проверка доступности комнат на выбранные даты
                var isAnyRoomAvailable = false;
                foreach (var room in rooms)
                {
                    var isRoomAvailable = await _context.Bookings
                        .AnyAsync(b => b.RoomId == room.Id &&
                                       (reservationDates.IsOverlapping(b.ReservationDates.ArrivalDate, b.ReservationDates.DepartureDate)
                                        || reservationDates.IsContained(b.ReservationDates.ArrivalDate, b.ReservationDates.DepartureDate)));
    
                    if (!isRoomAvailable)
                    {
                        isAnyRoomAvailable = true;
                        double hotelStarRating = hotel.HotelStarRating;
                        
                        decimal basePrice = room.Price;
                        decimal discount = (decimal)(hotelStarRating * 5.0);
    
                        decimal bookingPrice = basePrice * ( discount / 100);
                        
                        var booking = Booking.Create(BookingId.Of(Guid.NewGuid()), ReservationDates.Of(createBookingDto.ArrivalDate, createBookingDto.DepartureDate), 
                            NumberOfGuests.Of(createBookingDto.NumberOfGuests), hotel, room, Discount.Of(discount), BookingPrice.Of(bookingPrice));
    
                        await _context.Bookings.AddAsync(booking);
                        await _context.SaveChangesAsync();
    
                        return booking;
                    }
                }

                if (isAnyRoomAvailable)
                {
                    // Если есть доступные комнаты в отеле, но все заняты на выбранные даты, переходим к следующему отелю
                    continue;
                }
            }
        } 
        return await ToBook(createBookingDto);
    }

    public Task<BookingDto?> GetBookingById(string bookingId)
    {
        throw new NotImplementedException();
    }
    
}