using System.Security.Claims;
using Chat.API.Presence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs;

[Authorize]
public sealed class PresenceHub : Hub
{
    private readonly IPresenceTracker _presenceTracker;

    public PresenceHub(
        IPresenceTracker presenceTracker)
    {
        _presenceTracker = presenceTracker;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Guid.Parse(
            Context.User!
                .FindFirst(ClaimTypes.NameIdentifier)!
                .Value);

        var wasOnline = await _presenceTracker
            .IsOnlineAsync(userId);

        await _presenceTracker.UserConnectedAsync(
            userId,
            Context.ConnectionId);

        if (!wasOnline)
        {
            await Clients.All.SendAsync(
                "UserOnline",
                userId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(
        Exception? exception)
    {
        var userId = Guid.Parse(
            Context.User!
                .FindFirst(ClaimTypes.NameIdentifier)!
                .Value);

        var isOffline =
            await _presenceTracker
                .UserDisconnectedAsync(
                    userId,
                    Context.ConnectionId);

        if (isOffline)
        {
            await Clients.All.SendAsync(
                "UserOffline",
                userId);
        }

        await base.OnDisconnectedAsync(
            exception);
    }

    public async Task<IReadOnlyCollection<Guid>> GetOnlineUsers()
    {
        return await _presenceTracker.GetOnlineUsersAsync();
    }
}