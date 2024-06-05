using System.Text.Json;
using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Application.EventBus;
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
    private readonly KafkaProducerService _kafkaProducerService;

    public BookingRequestCommandHandler(IBookingService bookingService, ILogger<BookingRequestCommandHandler> logger,
        KafkaProducerService kafkaProducerService)
    {
        _bookingService = bookingService;
        _logger = logger;
        _kafkaProducerService = kafkaProducerService;
    }

    public async Task<BookingRequestResult> Handle(BookingRequestCommand request, CancellationToken cancellationToken)
    {
        var message = new BookingRecordDto(Guid.NewGuid(),
            request.ArrivalDate.ToUniversalTime().ToUnixTimeSeconds().ToString(),
            request.DepartureDate.ToUniversalTime().ToUnixTimeSeconds().ToString(),
            request.NumberOfGuests.ToString(),
            new DateTimeOffset(DateTime.UtcNow.Date.ToUniversalTime()).ToUnixTimeSeconds().ToString());
        await _kafkaProducerService.ProduceAsync("BookingRequest", JsonSerializer.Serialize(message));
        _logger.LogInformation("Received booking request for {NumberOfGuests} guests from {ArrivalDate} to {DepartureDate}",
            request.NumberOfGuests, request.ArrivalDate.ToString("dd.MM.yyyy"), request.DepartureDate.ToString("dd.MM.yyyy"));
        var booking = await _bookingService.AddBooking(request.ArrivalDate, request.DepartureDate, request.NumberOfGuests);
        return new BookingRequestResult(booking);
    }
}



