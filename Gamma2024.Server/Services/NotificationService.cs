using Gamma2024.Server.Data;
using Gamma2024.Server.Hub;
using Gamma2024.Server.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Services
{
    public class NotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ApplicationDbContext _context;

        public NotificationService(IHubContext<NotificationHub> hubContext, ApplicationDbContext context)
        {
            _hubContext = hubContext;
            _context = context;
        }

        public async Task SendBidNotification(int lotId, string userId, string message)
        {
            // Enregistrer la notification dans la base de données
            var notification = new Notification
            {
                ApplicationUserId = userId,
                Message = message,
                CreeA = DateTime.Now,
                estLu = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Envoyer la notification en temps réel via SignalR
            await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message);
        }

        public async Task<ICollection<Notification>> GetUnreadNotification(string userId)
        {
            var notifications = _context.Notifications
                                              .Where(n => n.ApplicationUserId == userId && !n.estLu)
                                              .OrderByDescending(n => n.CreeA)
                                              .ToList();
            if (notifications.Any())
            {
                return notifications;
            }
            return null;
        }

        public async Task<(bool Success, string Message)> MarquerNotificationLu()
        {
            var notifications = await _context.Notifications.ToListAsync();
            if (notifications == null)
            {
                return (false, "Aucune notification trouvé");
            }

            foreach (var notif in notifications)
            {
                notif.estLu = true;
            }

            _context.Notifications.UpdateRange(notifications);
            await _context.SaveChangesAsync();

            return (true, "Notification lu avec succès");
        }
    }
}
