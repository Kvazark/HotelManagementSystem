
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
using Consul;
using HotelManagementSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
{
    consulConfig.Address = new Uri("http://localhost:8500");
}));

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
builder.Services.Configure<ServiceDiscoveryConfig>(builder.Configuration.GetSection("ServiceDiscoveryConfig"));

var app = builder.Build();

builder.Services.AddLogging(e => e.AddConsole());

app.UseConsul();
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
    var message = "";
    
    foreach (var stats in statistics)
    {
        message += $"Отель: {stats.Hotel}\n";
        message += $"Всего броней: {stats.TotalBookings}\n";
        message += "Статистика по номерам комнат:\n";

        foreach (var roomStats in stats.RoomBookingStats)
        {
            message += $"  Номер комнаты: {roomStats.RoomNumber}, Количество броней: {roomStats.BookingCount}, Всего гостей: {roomStats.TotalGuests}\n";
        }

        message += $"Всего гостей: {stats.TotalGuests}\n\n";
    }
    
    return message;
});

app.Run();