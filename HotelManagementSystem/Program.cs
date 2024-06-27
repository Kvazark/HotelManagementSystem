
using System.Reflection;
using HotelManagementSystem.Application.Features;
using HotelManagementSystem.Application.Handlers;
using HotelManagementSystem.Application.Services;
using HotelManagementSystem.Data;
using HotelManagementSystem.DomainServices;
using HotelManagementSystem.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Confluent.Kafka;
using HotelManagementSystem.Application.EventBus;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddDbContext<HotelBookingContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=HotelBooking;Username=postgres;Password=root",
        b => b.MigrationsAssembly("HotelManagementSystem"));
});
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<KafkaProducerService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

builder.Services.AddLogging(e => e.AddConsole());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!");

app.MapPost("api/addBooking", async (CreateBookingDto createBookingDto,
    IBookingService bookingService, IMediator mediator) =>
{
    var command = new BookingRequestCommand(createBookingDto.ArrivalDate, createBookingDto.DepartureDate, 
        createBookingDto.NumberOfGuests);
    var response = await mediator.Send(command);
    return Results.Created($"api/addbooking/{response.booking.Id}", response);
});

app.MapPost("api/addNewRoom", async (CreateRoomDto createRoomDto, IRoomService roomService) =>
{
    var newRoom = await roomService.CreateNewRoom(createRoomDto.NumberRoom, 
        createRoomDto.RoomCategory,createRoomDto.Capacity, createRoomDto.BaseRoomPrice, createRoomDto.HotelId);
    return Results.Created($"api/addNewRoom/{newRoom.Id}", newRoom);
});

app.MapPost("api/addNewHotel", async (CreateHotelDto createHotelDto, IHotelService hotelService) =>
{
    var newHotel = await hotelService.CreateHotel(createHotelDto.Name, createHotelDto.Address);
    return Results.Created($"api/addNewHotel/{newHotel.Id}", newHotel);
});

app.MapPut("api/updateHotelStarRating", async (HotelRatingDto hotelDto, IHotelService hotelService) =>
{
    var updateHotel = await hotelService.updateHotelStarRating(hotelDto.Id, hotelDto.HotelStarRating);
    return Results.Created($"api/updateHotelStarRating/{hotelDto.Id}", updateHotel);
});

app.MapGet("api/getbookingById", async (Guid bookingId, IBookingService bookingService) =>
{
    var booking = await bookingService.GetBookingById(bookingId);

    return Results.Created($"api/getbookingbyid/{bookingId}", booking);
});

app.MapGet("api/getStatisticsBookings", async (IBookingService bookingService) =>
{
    var statistics = await bookingService.GetBookingStatsByHotel();

    foreach (var stats in statistics)
    {
        Console.WriteLine($"Отель: {stats.Hotel}");
        Console.WriteLine($"Всего броней: {stats.TotalBookings}");
        Console.WriteLine("Статистика по номерам комнат:");

        foreach (var roomStats in stats.RoomBookingStats)
        {
            Console.WriteLine($"  Номер комнаты: {roomStats.RoomNumber}, Количество броней: {roomStats.BookingCount}, Всего гостей: {roomStats.TotalGuests}");
        }

        Console.WriteLine($"Всего гостей: {stats.TotalGuests}");
        Console.WriteLine();
    }
});

app.Run();