using Microsoft.AspNetCore.SignalR;

namespace Gamma2024.Server.Hub
{
    public interface INotificationHub
    {
        Task ReceiveNotification(object value);
    }

    public class NotificationHub : Hub<INotificationHub>
    {
        public async Task SendNotification(string userId, string notifMessage)
        {
            await Clients.All.ReceiveNotification(new
            {
                ApplicationUserId = userId,
                message = notifMessage,
                CreatedAt = DateTime.Now
            });
        }
    }


}