using HotelManagementSystem.Common;

namespace HotelManagementSystem.Aggregates;

public class Hotel : Aggregate<HotelId>
{
    public Name Name { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public HotelStarRating HotelStarRating { get; private set; } = default!;
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

    public static async Task<Hotel> AddHotel(string name, string address)
    {
        var hotel = Hotel.Create(HotelId.Of(Guid.NewGuid()), Name.Of(name),
            Address.Of(address), HotelStarRating.Of(1));
        return hotel;
    }

    public async Task<Hotel> AddRoom(Room room, Hotel hotel) 
    {
        hotel.Rooms ??= new List<Room>(); 
        hotel.Rooms.Add(room);
        return hotel;
    }
}




