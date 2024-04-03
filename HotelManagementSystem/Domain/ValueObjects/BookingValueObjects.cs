namespace HotelManagementSystem.Aggregates;

/// <summary>
/// Order
/// </summary>
public class BookingId
{
    public Guid Value { get; }

    private BookingId(Guid value)
    {
        Value = value;
    }
    public static BookingId Of(Guid value)
    {
        if (value==Guid.Empty) throw new InvalidBookingIdException(value);
        return new BookingId(value);
    }

    public static implicit operator Guid(BookingId bookingId)
    {
        return bookingId.Value;
    }
}
public class ReservationDates
{
    public DateTime ArrivalDate { get; }
    public DateTime DepartureDate { get; }

    public ReservationDates(DateTime arrivalDate, DateTime departureDate)
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
    public bool IsOverlapping(DateTime otherArrivalDate, DateTime otherDepartureDate)
    {
        return ArrivalDate < otherDepartureDate && otherArrivalDate < DepartureDate;
    }

    public bool IsContained(DateTime otherArrivalDate, DateTime otherDepartureDate)
    {
        return ArrivalDate <= otherArrivalDate && otherDepartureDate <= DepartureDate;
    }
}

public class NumberOfGuests
{
    public int Value { get; }

    public NumberOfGuests(int  value)
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

public class Discount
{
    public decimal Value { get; }

    private Discount(decimal value)
    {
        Value = value;
    }
    public static Discount Of(decimal value)
    {
        if (value < 0) throw new InvalidDiscountException();
        return new Discount(value);
    }

    public static implicit operator decimal(Discount discount)
    {
        return discount.Value;
    }
}

public class BookingPrice
{
    public decimal Value { get; }

    private BookingPrice(decimal value)
    {
        Value = value;
    }
    public static BookingPrice Of(decimal value)
    {
        if (value < 0) throw new InvalidBookingPriceException();
        return new BookingPrice(value);
    }

    public static implicit operator decimal(BookingPrice bookingPrice)
    {
        return bookingPrice.Value;
    }
}




