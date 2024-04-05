using HotelManagementSystem.Aggregates;

namespace HotelManagementSystem.DTO;

public class CreateBookingDto
{
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public int NumberOfGuests { get; set; }
}