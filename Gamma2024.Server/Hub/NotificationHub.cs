namespace Gamma2024.Server.Hub;
using Microsoft.AspNetCore.SignalR;


public class NotificationHub : Hub
{
    // Méthode pour envoyer une notification aux utilisateurs
    public async Task SendNotification(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveNotification", message);
    }


    // Méthode pour notifier tous les utilisateurs ayant misé sur le lot
    public async Task NotifyUsers(string lotId, string message)
    {
        await Clients.Group(lotId).SendAsync("ReceiveNotification", message);
    }


    // Joindre un utilisateur à un groupe de lot spécifique
    public async Task JoinLotGroup(string lotId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, lotId);
    }

}

