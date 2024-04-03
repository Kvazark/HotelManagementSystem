namespace HotelManagementSystem.DTO;

public class HotelDto
{
    public Guid HotelId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double HotelStarRating { get; set; }
    private List<RoomDto>? Rooms { get; set; }
}