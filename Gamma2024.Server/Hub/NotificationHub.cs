using Microsoft.AspNetCore.SignalR;

namespace Gamma2024.Server.Hub{
    public interface INotificationHub
    {
        Task ReceiveNotification(string message);
    }

    public class NotificationHub : Hub<INotificationHub>
    {
        // Méthode pour envoyer une notification aux utilisateurs
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).ReceiveNotification(message);
        }


        // Méthode pour notifier tous les utilisateurs ayant misé sur le lot
        public async Task NotifyUsers(string lotId, string message)
        {
            await Clients.Group(lotId).ReceiveNotification(message);
        }


        // Joindre un utilisateur à un groupe de lot spécifique
        public async Task JoinLotGroup(string lotId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lotId);
        }

    }


}