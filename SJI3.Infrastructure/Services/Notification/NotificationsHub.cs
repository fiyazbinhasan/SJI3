using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace SJI3.Infrastructure.Services.Notification
{
    public class NotificationsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            if (Context.User is {Identity.Name: { }}) await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.User is {Identity.Name: { }}) await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
