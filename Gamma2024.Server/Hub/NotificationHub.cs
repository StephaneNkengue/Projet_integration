using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Gamma2024.Server.Hub
{
    public interface INotificationHub
    {
        Task ReceiveNotification(object value);
    }

    [Authorize]
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub//<INotificationHub>
    {
        public async Task SendNotification(string userId, string notifMessage)
        {
            await Clients.All.SendAsync("ReceiveNotification", new
            {
                ApplicationUserId = userId,
                message = notifMessage,
                CreatedAt = DateTime.Now
            });
        }

        public static Dictionary<string, List<UserConnection>> Mappings = [];

        public async Task SendToUser(string userId, Notification notification)
        {
            //await Clients.User(Mappings[userId].UserId).SendAsync("ReceiveNotification", notification);

            //await Clients.User(Mappings[userId].ConnectionId).SendAsync("ReceiveNotification", notification);

            var entry = Mappings.First(x => x.Key == userId);
            foreach (var obj in entry.Value)
            {
                await Clients.Client(obj.ConnectionId).SendAsync("ReceiveNotification", notification);

            }
        }


        public override Task OnConnectedAsync()
        {
            if (Mappings.ContainsKey(Context.UserIdentifier) == false)
            {
                Mappings.Add(Context.UserIdentifier, new());
            }

            Mappings[Context.UserIdentifier].Add(new() { ConnectionId = Context.ConnectionId, UserId = Context.UserIdentifier });
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            Mappings.Remove(Context.UserIdentifier);
            return base.OnDisconnectedAsync(exception);
        }

        public class UserConnection
        {
            public string UserId { get; set; }
            public string ConnectionId { get; set; }
        }

    }
}