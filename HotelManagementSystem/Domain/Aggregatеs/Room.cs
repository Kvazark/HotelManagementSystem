using HotelManagementSystem.Domain.Aggregatеs;
using HotelManagementSystem.DTO;


namespace HotelManagementSystem.Aggregates;

public class Room  
{
    public Guid Id { get; private set; } = default!;
    public NumberRoom NumberRoom { get; private set; } = default!;
    public RoomCategory RoomCategory { get; private set; } = default!;
    public Capacity Capacity { get; private set; } = default!;
    
    public RoomPrice RoomPrice { get; private set; } = default!;
    public Hotel Hotel { get; private set; } = default!;
    
    // public ICollection<Booking> Bookings { get; set; }

    public Room()
    {
        
    }

    public static Room Create(RoomId id, Hotel hotel, NumberRoom numberRoom, RoomCategory roomCategory, Capacity capacity, RoomPrice roomPrice, bool isDeleted = false)
    {
        var room = new Room
        {
            Id = id,
            Hotel = hotel,
            NumberRoom = numberRoom,
            Capacity = capacity,
            RoomCategory = roomCategory,
            RoomPrice = roomPrice
        };
        return room;
    }
    
    public static async Task<Room> CreateRoom(string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice, Hotel hotel)
    {
        var roomPrice = baseRoomPrice * RoomPrice.GetCoefficientByCategory(roomCategory);
        var room = Room.Create(RoomId.Of(Guid.NewGuid()), hotel, NumberRoom.Of(numberRoom), 
            RoomCategory.Of(roomCategory), Capacity.Of(capacity), RoomPrice.Of(roomPrice));
        return room;
    }
}