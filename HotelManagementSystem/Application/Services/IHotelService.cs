using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Aggregatеs;

namespace HotelManagementSystem.DomainServices;

public interface IHotelService
{
    public Task<Hotel> CreateHotel(string name, string address);

    public Task<Hotel?> AddRoom(Guid hotelId, string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice);

    public Task<Hotel?> GetHotelById(Guid hotelId);
}