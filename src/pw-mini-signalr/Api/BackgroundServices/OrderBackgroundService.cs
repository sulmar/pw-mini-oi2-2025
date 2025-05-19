using Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Api.BackgroundServices;

public class OrderBackgroundService(IHubContext<OrderHub> hub,
    ILogger<OrderBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var order = new Order(
                Random.Shared.Next(1, 9999), 
                 $"ZAM {Random.Shared.Next(1, 9999)}");

            logger.LogInformation("Created order {OrderId} with number {OrderNumber}", order.Id, order.OrderNumber);
            
            await hub.Clients.All.SendAsync("OrderCreated", order);
            
            await Task.Delay(1, stoppingToken);
        }
    }
}