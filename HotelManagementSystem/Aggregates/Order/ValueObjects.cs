namespace RestaurantManagementSystem.Aggregates;

/// <summary>
/// Order
/// </summary>
public class OrderId
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        Value = value;
    }
    public static OrderId Of(Guid value)
    {
        if (value==Guid.Empty) throw new InvalidOrderIdException(value);
        return new OrderId(value);
    }

    public static implicit operator Guid(OrderId orderId)
    {
        return orderId.Value;
    }
}
public class ReservationDates
{
    public DateTime ArrivalDate { get; }
    public DateTime DepartureDate { get; }
    private ReservationDates(DateTime arrivalDate, DateTime departureDate)
    {
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
    }
    public static ReservationDates Of(DateTime arrivalDate, DateTime departureDate)
    {
        if (departureDate <= arrivalDate)
        {
            throw new InvalidReservationDatesException();
        }
        return new ReservationDates(arrivalDate, departureDate);
    }
}

public class Service
{
    public string Name { get; }
    public decimal Price { get; }

    public Service(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
    
    public static Service Of(string name, decimal price)
    {
        return new Service(name, price);
    }
}

public class ServicesList
{
    public List<Service> Value { get; }

    private ServicesList(List<Service>  value)
    {
        Value = value;
    }
    public static ServicesList Of(List<Service>  value)
    {
        return new ServicesList(value);
    }
    public static implicit operator List<Service> (ServicesList servicesList)
    {
        return servicesList.Value;
    }
}

public class NumberOfGuests
{
    public int Value { get; }

    private NumberOfGuests(int  value)
    {
        Value = value;
    }
    public static NumberOfGuests Of(int  value)
    {
        if (int.IsNegative(value) || value == 0)
        {
            throw new InvalidNumberOfGuestsException();
        }
        return new NumberOfGuests(value);
    }
    public static implicit operator int (NumberOfGuests numberOfGuests)
    {
        return numberOfGuests.Value;
    }
}

public class OrderPrice
{
    public decimal Value { get; }

    private OrderPrice(decimal value)
    {
        Value = value;
    }
    public static OrderPrice Of(decimal value)
    {
        if (value < 0) throw new InvalidOrderPriceException();
        return new OrderPrice(value);
    }

    public static implicit operator decimal(OrderPrice orderPrice)
    {
        return orderPrice.Value;
    }
}




