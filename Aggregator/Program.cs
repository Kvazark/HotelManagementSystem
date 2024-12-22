using Consul;
using HotelManagementSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
{
    consulConfig.Address = new Uri("http://localhost:8500");
}));
builder.Services.Configure<ServiceDiscoveryConfig>(builder.Configuration.GetSection("ServiceDiscoveryConfig"));


var app = builder.Build();

app.UseConsul();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/testAggregator", async () =>
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7184/bookings");
        var response = await client.SendAsync(request);
        return TypedResults.Ok(await response.Content.ReadAsStringAsync());
    })
    .WithName("getTestAggregator")
    .WithOpenApi();

app.Run();
