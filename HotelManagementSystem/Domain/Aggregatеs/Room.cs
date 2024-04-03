using HotelManagementSystem.Common;
using HotelManagementSystem.Domain.Constants;

namespace HotelManagementSystem.Aggregates;

public class Room  : Aggregate<RoomId>
{
    public NumberRoom NumberRoom { get; private set; } = default!;
    public RoomCategory RoomCategory { get; private set; } = default!;
    public Capacity Capacity { get; private set; } = default!;
    
    public RoomPrice RoomPrice { get; private set; } = default!;
    public Hotel Hotel { get; private set; } = default!;
    
    public virtual ICollection<Booking> Bookings { get; set; }
    
    
    public static Room Create(RoomId id, Hotel hotelId, NumberRoom numberRoom, RoomCategory roomCategory, Capacity capacity, RoomPrice roomPrice, bool isDeleted = false)
    {
        var room = new Room
        {
            Id = id,
            Hotel = hotelId,
            NumberRoom = numberRoom,
            Capacity = capacity,
            RoomPrice = roomPrice
        };
        return room;
    }
}