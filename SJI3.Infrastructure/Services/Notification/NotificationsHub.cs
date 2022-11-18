using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SJI3.Infrastructure.Services.Notification;

[Authorize("SJI3ApiPolicy")]
public class NotificationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        if (Context.User is {Identity.Name: { }})
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.FindFirstValue("sub"));
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        if (Context.User is {Identity.Name: { }})
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.FindFirstValue("sub"));
        await base.OnDisconnectedAsync(exception);
    }
}