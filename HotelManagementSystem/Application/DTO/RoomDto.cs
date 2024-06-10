using MediatR;

namespace HotelManagementSystem.DTO;

public record RoomSelectedEventDto(Guid IdEvent, string NumberRoom, string RoomCategory, string HotelName, string requestCreated) : INotification;
public class RoomDto
{
    public RoomCategory RoomCategory { get; set; }
    public int Capacity { get; set; }
    public HotelDto Hotel { get; set; }
}
