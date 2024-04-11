using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Data;

namespace HotelManagementSystem.DomainServices;

public class RoomService : IRoomService
{
    private readonly HotelBookingContext _context;

    public RoomService(HotelBookingContext context)
    {
        _context = context;
    }

    public async Task<Room> CreateRoom(string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice, Guid hotelId)
    {
        var newRoom = Room.CreateRoom(numberRoom, roomCategory, capacity, baseRoomPrice, hotelId);

        if (newRoom != null)
        {
            _context.Rooms.Add(await newRoom);
            await _context.SaveChangesAsync();
        }
        
        return await newRoom;
    }
    

    public Task<Room?> GetRoomById(Guid roomId)
    {
        throw new NotImplementedException();
    }
}