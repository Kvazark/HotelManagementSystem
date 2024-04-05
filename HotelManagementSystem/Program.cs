
using HotelManagementSystem.Data;
using HotelManagementSystem.DomainServices;
using HotelManagementSystem.DTO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddDbContext<HotelBookingContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=HotelBooking;Username=postgres;Password=root",
        b => b.MigrationsAssembly("HotelBooking"));
});
builder.Services.AddScoped<IBookingService, BookingService>();

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
    return Results.Created($"api/addbooking/{newBooking.Id}", newBooking);
});

app.MapPost("api/getbookingById", async (Guid bookingId, IBookingService bookingService) =>
{
    var booking = await bookingService.GetBookingById(bookingId);

    // return Results.Created($"api/getbookingbyid/{booking.BookingId}", booking);
});

app.Run();