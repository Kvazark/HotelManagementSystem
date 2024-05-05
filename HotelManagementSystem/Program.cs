
using System.Reflection;
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

app.MapPost("api/addbooking", async (CreateBookingDto createBookingDto,IBookingService bookingService) =>
{
    var newBooking = await bookingService.AddBooking(createBookingDto.ArrivalDate, createBookingDto.DepartureDate, createBookingDto.NumberOfGuests);
    // await mediator.Send(new AddUserCommand(userDto));
    return Results.Created($"api/addbooking/{newBooking.Id}", newBooking);
});

app.MapPost("api/addNewRoom", async (CreateRoomDto createRoomDto, IRoomService roomService) =>
{
    var newRoom = await roomService.CreateNewRoom(createRoomDto.NumberRoom, 
        createRoomDto.RoomCategory,createRoomDto.Capacity, createRoomDto.BaseRoomPrice, createRoomDto.HotelId);
    return Results.Created($"api/addNewRoom/{newRoom.Id}", newRoom);
});

app.MapGet("api/getbookingById", async (Guid bookingId, IBookingService bookingService) =>
{
    var booking = await bookingService.GetBookingById(bookingId);

    return Results.Created($"api/getbookingbyid/{bookingId}", booking);
});

// app.MapGet("api/getRoom", async (IMediator mediator)=> await mediator.Send(new GetRoom()));

app.Run();