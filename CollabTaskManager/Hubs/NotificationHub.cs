﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CollabTaskManager.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}

