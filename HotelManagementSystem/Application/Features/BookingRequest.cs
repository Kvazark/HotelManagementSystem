using HotelManagementSystem.Aggregates;
using MediatR;
using HotelManagementSystem.Application.Services;
using HotelManagementSystem.DTO;

namespace HotelManagementSystem.Application.Features;

public record BookingRequestCommand(DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate, int NumberOfGuests) : IRequest<BookingRequestResult>
{
    
}

public record BookingRequestResult(Booking booking);

public class BookingRequestCommandHandler: IRequestHandler<BookingRequestCommand, BookingRequestResult>
{
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingRequestCommandHandler> _logger;

    public BookingRequestCommandHandler(IBookingService bookingService, ILogger<BookingRequestCommandHandler> logger)
    {
        _bookingService = bookingService;
        _logger = logger;
    }

    public async Task<BookingRequestResult> Handle(BookingRequestCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received booking request for {NumberOfGuests} guests from {ArrivalDate} to {DepartureDate}",
            request.NumberOfGuests, request.ArrivalDate.ToString("dd.MM.yyyy"), request.DepartureDate.ToString("dd.MM.yyyy"));
        var booking = await _bookingService.AddBooking(request.ArrivalDate, request.DepartureDate, request.NumberOfGuests);
        return new BookingRequestResult(booking);
    }
}



