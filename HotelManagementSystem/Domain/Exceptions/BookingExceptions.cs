namespace HotelManagementSystem.Aggregates;

public class InvalidBookingIdException : BadRequestException
{
    public InvalidBookingIdException(Guid bookingId)
        : base($"bookingId: '{bookingId}' is invalid.")
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

public class InvalidDiscountException : BadRequestException
{
    public InvalidDiscountException() : base("The discount of booking cannot be negative.")
    {
    }
}

public class InvalidBookingPriceException : BadRequestException
{
    public InvalidBookingPriceException() : base("The price of booking cannot be negative.")
    {
    }
}