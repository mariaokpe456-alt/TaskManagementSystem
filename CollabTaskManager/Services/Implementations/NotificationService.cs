//using CollabTaskManager.Hubs;
//using CollabTaskManager.Services.Interfaces;
//using Microsoft.AspNetCore.SignalR;
//using System.Threading.Tasks;

//namespace CollabTaskManager.Services.Implementations
//{
//    public class NotificationService : INotificationService
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;

//        public NotificationService(IHubContext<NotificationHub> hubContext)
//        {
//            _hubContext = hubContext;
//        }

//        public async Task NotifyTaskUpdated(string message)
//        {
//            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
//        }
//    }
//}

using CollabTaskManager.Hubs;
using CollabTaskManager.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CollabTaskManager.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IHubContext<NotificationHub> hubContext, ILogger<NotificationService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task NotifyTaskUpdated(string message)
        {
            try
            {
                _logger.LogInformation("Sending task update notification: {Message}", message);

                await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);

                _logger.LogInformation("Task update notification sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending task update notification.");
                throw new Exception("An error occurred while sending the notification.");
            }
        }
    }
}
