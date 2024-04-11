using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Data;

namespace HotelManagementSystem.DomainServices;

public class HotelService : IHotelService
{
    private readonly HotelBookingContext _context;

    public HotelService(HotelBookingContext context)
    {
        _context = context;
    }

    public async Task<Hotel> CreateHotel(string name, string address)
    {
        var newHotel = Hotel.AddHotel(name, address);

        if (newHotel != null)
        {
            _context.Hotels.Add(await newHotel);
            await _context.SaveChangesAsync();
        }
        
        return await newHotel;
    }

    public async Task<Hotel> AddRoom(Guid hotelId, string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice)
    {
        var hotel = _context.Hotels
            .FirstOrDefault(h => h.Id == hotelId);
        var newRoom = Hotel.AddRoom( hotel, numberRoom, roomCategory, capacity, baseRoomPrice);
        await _context.Add(newRoom);
        hotel.AddRoom(room);

        // Сохранить изменения в базе данных
        await _context.SaveChanges();
        return hotel;

    }

    public Task<Hotel?> GetHotelById(Guid hotelId)
    {
        throw new NotImplementedException();
    }
}