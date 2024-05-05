namespace HotelManagementSystem.DTO;

public record BookingRecordDto(DateTime ArrivalDate, DateTime DepartureDate, int NumberOfGuests);

public record BookingConfirmedDto(DateTime ArrivalDate, DateTime DepartureDate, int NumberOfGuests);

public class BookingDto
{
    public ReservationDatesDto ReservationDates { get; set; }
    public int NumberOfGuests { get; set; }
    
    public string NumberOfRoom { get; set; }
    public string CategoryRoom { get; set; }
    public string NameHotel { get; set; }
    public double HotelStarRating { get; set; }
    public string Address { get; set; }
    public decimal Discount  { get; set; } 
    public decimal FinalPrice  { get; set; } 

}