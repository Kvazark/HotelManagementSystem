namespace RestaurantManagementSystem.Aggregates;

public class InvalidOrderIdException : BadRequestException
{
    public InvalidOrderIdException(Guid orderId)
        : base($"orderId: '{orderId}' is invalid.")
    {
    }
}

public class InvalidReservationDatesException : BadRequestException
{
    public InvalidReservationDatesException() : base("The departure date must be after the arrival date.")
    {
    }
}

public class InvalidNumberOfGuestsException : BadRequestException
{
    public InvalidNumberOfGuestsException() : base("The number of guests cannot be negative or equal to zero.")
    {
    }
}

public class InvalidOrderPriceException : BadRequestException
{
    public InvalidOrderPriceException() : base("The price of order cannot be negative.")
    {
    }
}