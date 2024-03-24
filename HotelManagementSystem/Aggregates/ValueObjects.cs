public enum RoomCategory
{
    Standard,
    JuniorSuite,
    Deluxe,
    Suite
}
// using System.Runtime.InteropServices.JavaScript;
//
// namespace RestaurantManagementSystem.Aggregates;
//
// /// <summary>
// /// Common
// /// </summary>
// public class Name
// {
//     public string Value { get; }
//
//     private Name(string value)
//     {
//         Value = value;
//     }
//     public static Name Of(string value)
//     {
//         if (string.IsNullOrWhiteSpace(value)) throw new InvalidNameException();
//         return new Name(value);
//     }
//
//     public static implicit operator string(Name name)
//     {
//         return name.Value;
//     }
// }
//
// public class Address
// {
//     public string Value { get; }
//
//     private Address(string value)
//     {
//         Value = value;
//     }
//     public static Address Of(string value)
//     {
//         if (string.IsNullOrWhiteSpace(value)) throw new InvalidAddressException();
//         return new Address(value);
//     }
//
//     public static implicit operator string(Address address)
//     {
//         return address.Value;
//     }
// }
//
// public enum RoomCategory
// {
//     Standard,
//     JuniorSuite,
//     Deluxe,
//     Suite
// }
//
// public class InvalidAddressException : Exception
// {
// }
//
// public class InvalidNameException : Exception
// {
// }
// /// <summary>
// /// Hotel
// /// </summary>
// public class HotelId
// {
//     public Guid Value { get; }
//
//     private HotelId(Guid value)
//     {
//         Value = value;
//     }
//     public static HotelId Of(Guid value)
//     {
//         if (value==Guid.Empty) throw new InvalidHotelIdException();
//         return new HotelId(value);
//     }
//
//     public static implicit operator Guid(HotelId hotelId)
//     {
//         return hotelId.Value;
//     }
// }
//
// public class HotelStarRating
// {
//     public double  Value { get; }
//
//     private HotelStarRating(double  value)
//     {
//         Value = value;
//     }
//     public static HotelStarRating Of(double  value)
//     {
//         if (value < 1 || value > 5) throw new InvalidHotelStarRatingException();
//         return new HotelStarRating(value);
//     }
//     public static implicit operator double (HotelStarRating hotelStarRating)
//     {
//         return hotelStarRating.Value;
//     }
// }
//
// public class RoomQuantity
// {
//     public RoomCategory Category { get; }
//     public int Quantity { get; }
//
//     private RoomQuantity(RoomCategory category, int quantity)
//     {
//         Category = category;
//         Quantity = quantity;
//     }
//     public static RoomQuantity Of(RoomCategory category, int quantity)
//     {
//         if (quantity < 0) throw new ArgumentException("Количество номеров не может быть отрицательным.");
//         return new RoomQuantity(category, quantity);
//     }
// }
//
// // public class HotelAmenities
// // {
// //     public List<string> Value { get; }
// //
// //     private HotelAmenities(List<string>  value)
// //     {
// //         Value = value;
// //     }
// //     public static HotelAmenities Of(List<string>  value)
// //     {
// //         if (List<string>.) throw new InvalidHotelAmenitiesException();
// //         return new HotelAmenities(value);
// //     }
// //     public static implicit operator double (HotelAmenities hotelAmenities)
// //     {
// //         return hotelAmenities.Value;
// //     }
// // }
//
// public class InvalidHotelAmenitiesException : Exception
// {
// }
//
// public class InvalidHotelIdException : Exception
// {
// }
// public class InvalidHotelStarRatingException : Exception
// {
//     // return new ArgumentException("Значение количества звезд должно быть от 1 до 5.");
// }
// /// <summary>
// /// Room
// /// </summary>
//
// public class RoomId
// {
//     public Guid Value { get; }
//
//     private RoomId(Guid value)
//     {
//         Value = value;
//     }
//     public static RoomId Of(Guid value)
//     {
//         if (value==Guid.Empty) throw new InvalidRoomIdException();
//         return new RoomId(value);
//     }
//
//     public static implicit operator Guid(RoomId roomId)
//     {
//         return roomId.Value;
//     }
// }
//
//
// public class PriceRoom
// {
//     public double  Value { get; }
//
//     private PriceRoom(double  value)
//     {
//         Value = value;
//     }
//     public static PriceRoom Of(double  value)
//     {
//         if (value < 1 || value > 5) throw new InvalidPriceRoomException();
//         return new PriceRoom(value);
//     }
//     public static implicit operator double (PriceRoom priceRoom)
//     {
//         return priceRoom.Value;
//     }
// }
//
// public class InvalidRoomIdException : Exception
// {
// }
// public class InvalidPriceRoomException : Exception
// {
// }
// /// <summary>
// /// Order
// /// </summary>
// public class OrderId
// {
//     public Guid Value { get; }
//
//     private OrderId(Guid value)
//     {
//         Value = value;
//     }
//     public static OrderId Of(Guid value)
//     {
//         if (value==Guid.Empty) throw new InvalidOrderIdException();
//         return new OrderId(value);
//     }
//
//     public static implicit operator Guid(OrderId orderId)
//     {
//         return orderId.Value;
//     }
// }
// public class ReservationDates
// {
//     public DateTime ArrivalDate { get; }
//     public DateTime DepartureDate { get; }
//     private ReservationDates(DateTime arrivalDate, DateTime departureDate)
//     {
//         ArrivalDate = arrivalDate;
//         DepartureDate = departureDate;
//     }
//     public static ReservationDates Of(DateTime arrivalDate, DateTime departureDate)
//     {
//         if (departureDate <= arrivalDate)
//         {
//             throw new InvalidReservationDatesException("Дата отбытия должна быть после даты прибытия.");
//         }
//         return new ReservationDates(arrivalDate, departureDate);
//     }
// }
//
// public class ServicesList
// {
//     public List<string> Value { get; }
//
//     private ServicesList(List<string>  value)
//     {
//         Value = value;
//     }
//     public static ServicesList Of(List<string>  value)
//     {
//         return new ServicesList(value);
//     }
//     public static implicit operator List<string> (ServicesList servicesList)
//     {
//         return servicesList.Value;
//     }
// }
//
// public class NumberOfGuests
// {
//     public int Value { get; }
//
//     private NumberOfGuests(int  value)
//     {
//         Value = value;
//     }
//     public static NumberOfGuests Of(int  value)
//     {
//         if (int.IsNegative(value) || value == 0)
//         {
//             throw new InvalidNumberOfGuestsException();
//         }
//         return new NumberOfGuests(value);
//     }
//     public static implicit operator int (NumberOfGuests numberOfGuests)
//     {
//         return numberOfGuests.Value;
//     }
// }
//
// public class InvalidNumberOfGuestsException : Exception
// {
// }
//
// public class InvalidReservationDatesException : Exception
// {
//     public InvalidReservationDatesException(string mes)
//     {
//         throw new NotImplementedException();
//     }
// }
//
// public class InvalidOrderIdException : Exception
// {
// }
//
//
//
