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

public class HotelBookingStatsDto
{
    public string Hotel { get; set; }
    public int TotalBookings { get; set; }
    public List<RoomBookingStats> RoomBookingStats { get; set; }
    public int TotalGuests { get; set; }
}

public class RoomBookingStats
{
    public string RoomNumber { get; set; }
    public int BookingCount { get; set; }
    public int TotalGuests { get; set; }
}