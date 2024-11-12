using Microsoft.AspNetCore.SignalR;

namespace Gamma2024.Server.Hub {
    public interface ILotMiseHub
    {
        Task ReceiveNewBid(object bid);
    }

    public class LotMiseHub : Hub<ILotMiseHub>
    {
        public async Task PlaceBid(int lotId, decimal amount, string userId)
        {
            await Clients.All.ReceiveNewBid(new
            {
                idLot = lotId,
                montant = amount,
                userId = userId
            });
        }
    }
}
