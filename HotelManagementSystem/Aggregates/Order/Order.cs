using RestaurantManagementSystem.Common;

namespace RestaurantManagementSystem.Aggregates;

public class Order : Aggregate<OrderId>
{
    public ReservationDates ReservationDates { get; private set; } = default!;
    public ServicesList Services { get; private set; } = default!;
    public NumberOfGuests NumberOfGuests { get; private set; } = default!;
    
    //нужен ли здесь?
    public OrderPrice OrderPrice  { get; private set; } = default!;
}