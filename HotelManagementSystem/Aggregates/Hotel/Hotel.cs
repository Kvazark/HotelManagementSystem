using RestaurantManagementSystem.Common;

namespace RestaurantManagementSystem.Aggregates;

public class Hotel : Aggregate<HotelId>
{
    public Name Name { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public HotelStarRating HotelStarRating { get; private set; } = default!;
    public RoomQuantity RoomQuantity { get; private set; } = default!;
    
    
    // public HotelAmenities HotelAmenities { get; private set; } = default!;
    // public static Hotel Create(HotelId id, Name name, Address address, ... , bool isDeleted = false)
    // {
    //     var hotel = new Hotel
    //     {
    //         Id = id,
    //         Name = name,
    //         Address = address,
    //          ...
    //     };
    //
    //     return hotel;
    // }
}




