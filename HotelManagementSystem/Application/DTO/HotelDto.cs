namespace HotelManagementSystem.DTO;

public class HotelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double HotelStarRating { get; set; }
}

public class HotelRatingDto
{
    public Guid Id { get; set; }
    public double HotelStarRating { get; set; }
}