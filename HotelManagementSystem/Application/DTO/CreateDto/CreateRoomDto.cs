namespace HotelManagementSystem.DTO;

public class CreateRoomDto
{
    public Guid HotelId { get; set; }
    public int Capacity { get; set; }
    public string NumberRoom { get; set; }
    public string RoomCategory { get; set; }
    public decimal BaseRoomPrice { get; set; }
}