using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Data;
using HotelManagementSystem.Domain.Aggregatеs;
using HotelManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.DomainServices;

public class RoomService : IRoomService
{
    private readonly HotelBookingContext _context;

    public RoomService(HotelBookingContext context)
    {
        _context = context;
    }

    public async Task<Room> CreateNewRoom(string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice, Guid hotelId)
    {
        var hotel = await _context.Hotels
            .Where(h => h.Id == hotelId)
            .FirstOrDefaultAsync();

        if (hotel != null)
        {
            var newRoom = Room.CreateRoom(numberRoom, roomCategory, capacity, baseRoomPrice, hotel);

            if (newRoom != null)
            {
                _context.Rooms.Add(await newRoom);
                await _context.SaveChangesAsync();
            }
        
            return await newRoom;
        }

        return null;
    }

    public Task<Room?> GetRoomById(Guid roomId)
    {
        throw new NotImplementedException();
    }
}