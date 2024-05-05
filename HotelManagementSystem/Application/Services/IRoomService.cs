using HotelManagementSystem.Aggregates;

namespace HotelManagementSystem.DomainServices;

public interface IRoomService
{
    public Task<Room> CreateNewRoom(string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice, Guid hotelId);
    
    public Task<Room?> GetRoomById(Guid roomId);
}