using System.Text.Json;
using HotelManagementSystem.DTO;
using MediatR;

namespace HotelManagementSystem.Domain.Events.Booking;

public class AddNewBooking
{
    
}
public record AddBookingCommand(BookingRecordDto BookingRecordDto):IRequest<string>;
// public class AddBookingCommandHandler(IProducer<string, string> producer) : IRequestHandler<AddBookingCommand, string>
// {
//     private readonly IProducer<string, string> _producer = producer;
//     private const string Topic = "bookingConfirmed-events";
//     
//     public async Task<string> Handle(AddBookingCommand request, CancellationToken cancellationToken)
//     {
//         var reservation = new BookingRecordDto(
//             ArrivalDate = request.ArrivalDate,
//             DepartureDate = request.DepartureDate,
//             HotelId = request.HotelId
//         );
//
//         var kafkaMessage = new Message<string, string>
//         {
//             Value = JsonSerializer.Serialize(reservation)
//         };
//
//         await _producer.ProduceAsync(Topic, kafkaMessage);
//
//         return $"Reservation created for Hotel {request.HotelId} from {request.ArrivalDate:yyyy-MM-dd} to {request.DepartureDate:yyyy-MM-dd}";
//     }
// }