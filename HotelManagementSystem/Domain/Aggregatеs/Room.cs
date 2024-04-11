using HotelManagementSystem.Common;
using HotelManagementSystem.Domain.Constants;

namespace HotelManagementSystem.Aggregates;

public class Room  : Aggregate<RoomId>
{
    public NumberRoom NumberRoom { get; private set; } = default!;
    public RoomCategory RoomCategory { get; private set; } = default!;
    public Capacity Capacity { get; private set; } = default!;
    
    public RoomPrice RoomPrice { get; private set; } = default!;
    public HotelId HotelId { get; private set; } = default!;


    public static Room Create(RoomId id, HotelId hotelId, NumberRoom numberRoom, RoomCategory roomCategory, Capacity capacity, RoomPrice roomPrice, bool isDeleted = false)
    {
        var room = new Room
        {
            Id = id,
            HotelId = hotelId,
            NumberRoom = numberRoom,
            Capacity = capacity,
            RoomPrice = roomPrice
        };
        return room;
    }
    
    public static async Task<Room> CreateRoom(string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice, Guid hotelId)
    {
        var roomPrice = baseRoomPrice * RoomPrice.GetCoefficientByCategory(roomCategory);
        var room = Create(RoomId.Of(Guid.NewGuid()), HotelId.Of(hotelId), NumberRoom.Of(numberRoom), 
            RoomCategory.Of(roomCategory), Capacity.Of(capacity), RoomPrice.Of(roomPrice));
        return room;
    }
}