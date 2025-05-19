using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Hello, SignalR Client!");

// dotnet add package Microsoft.AspNetCore.SignalR.Client

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5064/signalr/order")
    .Build();

connection.On<Order>("OrderCreated", order =>
{
    Console.WriteLine($"Received order {order.OrderNumber}");
});

Console.WriteLine("Connecting to hub...");
await connection.StartAsync();

Console.WriteLine("Connected.");

Console.WriteLine("Press any key to exit.");

Console.Read();

await connection.StopAsync();

record Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
}