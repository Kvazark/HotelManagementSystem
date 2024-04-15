﻿using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.DomainServices;

public class HotelService : IHotelService
{
    private readonly HotelBookingContext _context;
    private readonly IRoomService _roomService;

    public HotelService(HotelBookingContext context, IRoomService roomService)
    {
        _context = context;
        _roomService = roomService;
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

    public async Task<Hotel?> AddRoom(Guid hotelId, string numberRoom, string roomCategory, int capacity, decimal baseRoomPrice)
    {
        var hotel = await _context.Hotels
            .Include(h => h.Rooms) 
            .FirstOrDefaultAsync(h => h.Id == hotelId);
        
        if (hotel != null)
        {
            var newRoom = await _roomService.CreateRoom(numberRoom, roomCategory, capacity, baseRoomPrice, hotelId);
            if (newRoom != null)
            {
                await hotel.AddRoom(newRoom, hotel);
            }
            _context.Update(hotel);
            await _context.SaveChangesAsync();
        }
        
        return hotel;
    }

    public async Task<Hotel?> GetHotelById(Guid hotelId)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == hotelId);
        return hotel;
    }
}