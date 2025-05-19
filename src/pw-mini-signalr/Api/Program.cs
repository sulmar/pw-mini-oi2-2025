using Api.BackgroundServices;
using Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<OrderBackgroundService>();

builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHub<OrderHub>("/signalr/order");

app.Run();


record Order(int Id, string OrderNumber);
