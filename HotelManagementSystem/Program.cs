
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

app.MapPost("api/publications", async (PublicationInputDto publication, IUserService userService) =>
{
    var newPublication = await userService.AddPublication(publication.TextContent, publication.MediaContent, publication.UserGuidId);

    return Results.Created($"api/publications/{newPublication.Id}",newPublication);
});

app.MapPost("api/getbooking", async (Guid curBookingId, Guid userId, IBookingService bookingService) =>
{
    var listPublication = await userService.GetListPublications(curUserId, userId);

    return Results.Created($"api/getpublications/", listPublication);
});

app.Run();