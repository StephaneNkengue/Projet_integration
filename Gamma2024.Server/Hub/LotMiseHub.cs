namespace Gamma2024.Server.Hub;
using Microsoft.AspNetCore.SignalR;

public class LotMiseHub : Hub
{
    public async Task PlaceBid(int lotId, decimal amount, string userId)
    {
        await Clients.Caller.SendAsync("ConfirmBid", lotId, amount);

        await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveBid", lotId, amount, userId);
    }

}
