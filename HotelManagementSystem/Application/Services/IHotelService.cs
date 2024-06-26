﻿using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Aggregatеs;

namespace HotelManagementSystem.DomainServices;

public interface IHotelService
{
    public Task<Hotel> CreateHotel(string name, string address);
    
    public Task<Hotel> updateHotelStarRating(Guid hotelId, double starRating);

    public Task<Hotel?> AddRoom(Guid hotelId, int capacity, string numberRoom, string roomCategory, decimal baseRoomPrice);

    public Task<Hotel?> GetHotelById(Guid hotelId);
}