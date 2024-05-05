
using System.Reflection;
using HotelManagementSystem.Application.Features;
using HotelManagementSystem.Application.Services;
using HotelManagementSystem.Data;
using HotelManagementSystem.DomainServices;
using HotelManagementSystem.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// builder.Services.AddQuartz(q =>
// {
//     q.UseMicrosoftDependencyInjectionJobFactory();
//     // Just use the name of your job that you created in the Jobs folder.
//     q.AddJob<AddRandomFriendsJob>(AddRandomFriendsJob.Key);
//
//     q.AddTrigger(opts => opts
//         .ForJob(AddRandomFriendsJob.Key)
//         .WithIdentity("AddRandomFriendsJob-startTrigger")
//         .WithSimpleSchedule(x => x
//             .WithIntervalInMinutes(1)
//             .RepeatForever())
//         .StartNow()
//     );
// });
// builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
// //-------------Add Kafka--------------------//
// var producerConfig = new ProducerConfig
// {
//     // BootstrapServers = $"localhost:29092",
//     // ClientId = "emailApprover-producer"
// };
//
// var consumerConfig = new ConsumerConfig
// {
//     // BootstrapServers = $"localhost:29092",
//     // GroupId = "emailApprover-consumer",
//     AutoOffsetReset = AutoOffsetReset.Earliest
// };
// ////////
// builder.Services.AddSingleton(new ProducerBuilder<string, string>(producerConfig).Build());
// builder.Services.AddSingleton(new ConsumerBuilder<string, string>(consumerConfig).Build());

//////////////////////////////////
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!");

app.MapPost("api/addbooking", async (CreateBookingDto createBookingDto,IBookingService bookingService, IMediator mediator) =>
{
    //var newBooking = await bookingService.AddBooking(createBookingDto.ArrivalDate, createBookingDto.DepartureDate, createBookingDto.NumberOfGuests);
    // await mediator.Send(new AddUserCommand(userDto));
    var command = new BookingRequestCommand(createBookingDto.ArrivalDate, createBookingDto.DepartureDate, createBookingDto.NumberOfGuests);
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

// app.MapGet("api/getRoom", async (IMediator mediator)=> await mediator.Send(new GetRoom()));

app.Run();