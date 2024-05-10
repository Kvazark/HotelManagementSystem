using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Aggregatеs;
using HotelManagementSystem.Domain.Common;
using MediatR;

namespace HotelManagementSystem.Application.Features;

public record SelectRoomCommand(List<Room> rooms, List<Booking> bookings, ReservationDates reservationDates) : IRequest<SelectRoomResult>
{
    
}

public record SelectRoomResult(bool IsRoomAvailable, Room room)
{
    
}   

public class SelectRoomCommandHandler: IRequestHandler<SelectRoomCommand, SelectRoomResult>
{

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
                Console.WriteLine("////////////////Room is selected!!!///////////////////////");
                return new SelectRoomResult(true,room);
            }
        }
        Console.WriteLine("////////////////Room is NOT selected!///////////////");
        return new SelectRoomResult(false, null);
    }
}
