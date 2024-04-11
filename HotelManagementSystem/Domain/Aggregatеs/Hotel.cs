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

    public static Task<Room> AddRoom(Hotel hotel, string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice)
    {
        var room = Room.CreateRoom(numberRoom, roomCategory, capacity, baseRoomPrice, hotel.Id);
        Rooms.Add(room);
        return room;
    }
}




