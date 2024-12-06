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


        private static readonly Dictionary<string, HashSet<string>> _userConnections = new();

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier!;
            var connectionId = Context.ConnectionId;

            lock (_userConnections)
            {
                if (!_userConnections.ContainsKey(userId))
                {
                    _userConnections[userId] = [];
                }
                _userConnections[userId].Add(connectionId);
            }

            await Clients.All.SendAsync("ReceiveNotification", $"Say hello to [{userId}] with connectionId [{connectionId}]");
            var user = Context.User;
            // Log authentication details
            Console.WriteLine($"User connected: {user?.Identity?.Name}, IsAuthenticated: {user?.Identity?.IsAuthenticated}");

            await base.OnConnectedAsync();
        }

        public async Task SendToUser(Notification notification)
        {
            var senderUserId = Context.UserIdentifier!;
            var targetUserId = _userConnections.Keys.First(x => x != senderUserId);
            await Clients.User(targetUserId).SendAsync("ReceiveNotification", notification);
        }

        public override async Task OnDisconnectedAsync(Exception? ex)
        {
            var userId = Context.UserIdentifier!;
            var connectionId = Context.ConnectionId;

            lock (_userConnections)
            {
                if (_userConnections.ContainsKey(userId))
                {
                    _userConnections[userId].Remove(connectionId);
                    // Remove the user entirely if they have no more connections
                    if (_userConnections[userId].Count == 0)
                    {
                        _userConnections.Remove(userId);
                    }
                }
            }

            await Clients.All.SendAsync("justDisonnected", $"Say bye to [{userId}] with connectionId [{connectionId}]");
            await base.OnDisconnectedAsync(ex);
        }

        // Helper method to get all connections for a user
        public HashSet<string> GetUserConnections(string userId)
        {
            lock (_userConnections)
            {
                return _userConnections.GetValueOrDefault(userId, new HashSet<string>());
            }
        }

    }
}