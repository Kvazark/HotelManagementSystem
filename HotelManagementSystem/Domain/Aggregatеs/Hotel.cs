using HotelManagementSystem.Common;

namespace HotelManagementSystem.Aggregates;

public class Hotel : Aggregate<HotelId>
{
    public Name Name { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public HotelStarRating HotelStarRating { get; private set; } = default!;
    
    public virtual ICollection<Booking> Bookings { get; set; }
    
    public virtual ICollection<Room> Rooms { get; set; }
    
    public static Hotel Create(HotelId id, Name name, Address address, HotelStarRating hotelStarRating, bool isDeleted = false)
    {
        var hotel = new Hotel
        {
            Id = id,
            Name = name,
            Address = address,
            HotelStarRating = hotelStarRating,
        };
        return hotel;
    }
    
    // public static Hotel AddedRoom(RoomId roomId,)
}




