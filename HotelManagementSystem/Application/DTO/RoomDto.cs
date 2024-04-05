using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Constants;

namespace HotelManagementSystem.DTO;

public class RoomDto
{
    public Guid RoomId { get; set; }
    public RoomCategory RoomCategory { get; set; }
    public int Capacity { get; set; }
    public HotelDto Hotel { get; set; }
}
