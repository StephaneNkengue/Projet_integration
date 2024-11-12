using Microsoft.AspNetCore.SignalR;
using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Hub {
    public interface ILotMiseHub
    {
        Task ReceiveNewBid(object bid);
        Task UpdateBidStatus(object status);
    }

    public class LotMiseHub : Hub<ILotMiseHub>
    {
        private readonly ApplicationDbContext _context;

        public LotMiseHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task PlaceBid(int lotId, decimal amount, string userId)
        {
            // Envoyer la nouvelle mise
            await Clients.All.ReceiveNewBid(new
            {
                idLot = lotId,
                montant = amount,
                userId = userId,
                timestamp = DateTime.Now
            });

            // Récupérer l'historique des mises pour ce lot
            var previousBidders = await _context.MiseAutomatiques
                .Where(m => m.LotId == lotId)
                .OrderByDescending(m => m.DateMise)
                .Select(m => m.UserId)
                .Distinct()
                .ToListAsync();

            // Envoyer les statuts de mise à jour
            await Clients.All.UpdateBidStatus(new
            {
                lotId = lotId,
                currentBidderId = userId,
                previousBidders = previousBidders
            });
        }
    }
}
