using HotelManagementSystem.Aggregates;
using MediatR;
using HotelManagementSystem.Application.Services;
using HotelManagementSystem.DTO;

namespace HotelManagementSystem.Application.Features;

public record BookingRequestCommand(DateTimeOffset ArrivalDate, DateTimeOffset DepartureDate, int NumberOfGuests) : IRequest<BookingRequestResult>
{
    // public Guid Id { get; init; } = Guid.NewGuid();
}

public record BookingRequestResult(Booking booking);

public class BookingRequestCommandHandler: IRequestHandler<BookingRequestCommand, BookingRequestResult>
{
    private readonly IBookingService _bookingService;

    public BookingRequestCommandHandler(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public async Task<BookingRequestResult> Handle(BookingRequestCommand request, CancellationToken cancellationToken)
    {
        var booking = await _bookingService.AddBooking(request.ArrivalDate, request.DepartureDate, request.NumberOfGuests);

        return new BookingRequestResult(booking);
    }
}



