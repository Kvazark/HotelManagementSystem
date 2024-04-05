using HotelManagementSystem.Aggregates;

namespace HotelManagementSystem.DTO;

public class BookingDto
{
    public ReservationDatesDto ReservationDates { get; set; } 
    public int NumberOfGuests { get; set; }
    public decimal Discount  { get; set; } 
    public decimal BookingPrice  { get; set; } 
    
}