using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

public class OrderHub(ILogger<OrderHub> logger) : Hub
{
    public override Task OnConnectedAsync()
    {
        // zła praktyka
       //  logger.LogInformation($"Client connected {Context.ConnectionId}");
        
        // dobra praktyka
        logger.LogInformation("Client connected {ConnectionId}", Context.ConnectionId);
        
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation("Client disconnected {ConnectionId}", Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}