using System.Text.Json;
using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Application.Handlers;
using HotelManagementSystem.Domain.Aggregatеs;
using HotelManagementSystem.Domain.Common;
using HotelManagementSystem.Domain.Events;
using HotelManagementSystem.DTO;
using MediatR;

namespace HotelManagementSystem.Application.Features;

public record SelectRoomCommand(List<Room> rooms, List<Booking> bookings, ReservationDates reservationDates, Hotel hotel) : IRequest<SelectRoomResult>
{
    
}

public record SelectRoomResult(bool IsRoomAvailable, Room room)
{
    
}   

public class SelectRoomCommandHandler: IRequestHandler<SelectRoomCommand, SelectRoomResult>
{
    private readonly ILogger<SelectRoomCommandHandler> _logger;
    private readonly IMediator _mediator;
    
    public SelectRoomCommandHandler(ILogger<SelectRoomCommandHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    public async Task<SelectRoomResult> Handle(SelectRoomCommand request, CancellationToken cancellationToken)
    {
        request.rooms.Shuffle();

        foreach (var room in request.rooms)
        {
            var isRoomAvailable = true;
            foreach (var booking in request.bookings)
            {
                var isBookingsOverlap = request.reservationDates.IsOverlapping(booking.ReservationDates.ArrivalDate,
                    booking.ReservationDates.DepartureDate);
                var isCurrentBookingContained = request.reservationDates.IsContained(booking.ReservationDates.ArrivalDate,
                    booking.ReservationDates.DepartureDate);
                var isRoomAlreadyBooked = booking.Room.Id == room.Id;

                if ((isBookingsOverlap || isCurrentBookingContained) && isRoomAlreadyBooked)
                {
                    isRoomAvailable = false;
                }
            }

            if (isRoomAvailable)
            {
                var roomSelectedEvent = new RoomSelectedDomainEvent(room);
                await _mediator.Publish(roomSelectedEvent);
                
                _logger.LogInformation("Selected room {RoomNumber} in hotel '{HotelName}' at {HotelAddress}",
                    room.NumberRoom.Value, request.hotel.Name.Value, request.hotel.Address.Value);
                return new SelectRoomResult(true,room);
            }
        }

        _logger.LogInformation(
            "There was no room available at the {hotelName} hotel for the period of time: from {arrivalDate} to {departureDate}",
            request.hotel.Name.Value, request.reservationDates.ArrivalDate.ToString("dd.MM.yyyy"), request.reservationDates.DepartureDate.ToString("dd.MM.yyyy"));
        throw new ArgumentOutOfRangeException("Sorry, there are no empty seats...");
        return new SelectRoomResult(false, null);
    }
}
