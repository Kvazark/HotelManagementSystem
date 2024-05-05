using HotelManagementSystem.Aggregates;

namespace HotelManagementSystem.Domain.Aggregatеs;

public class Hotel 
{
    public Guid Id { get; private set; } = default!;
    public Name Name { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public HotelStarRating HotelStarRating { get; private set; } = default!;
    // public virtual ICollection<Room> Rooms { get; set; }

    public Hotel()
    {
        
    }
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

    public static async Task<Hotel> AddRoom(int capacity, string numberRoom, string roomCategory, decimal baseRoomPrice, Hotel hotel) 
    {
        // hotel.Rooms ??= new List<Room>();
        // return Room.CreateRoom(numberRoom, roomCategory, capacity, baseRoomPrice, hotel);
        // hotel.Rooms.Add(await room);
        return hotel;
    }
}




