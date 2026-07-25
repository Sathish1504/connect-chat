using System.Collections.Concurrent;

namespace Chat.API.Presence;

public sealed class InMemoryPresenceTracker
    : IPresenceTracker
{
    private readonly ConcurrentDictionary<Guid, HashSet<string>>
        _connections = new();

    private readonly object _lock = new();

    public Task UserConnectedAsync(
        Guid userId,
        string connectionId)
    {
        lock (_lock)
        {
            if (!_connections.TryGetValue(
                    userId,
                    out var userConnections))
            {
                userConnections = [];

                _connections[userId] =
                    userConnections;
            }

            userConnections.Add(connectionId);
        }

        return Task.CompletedTask;
    }

    public Task<bool> UserDisconnectedAsync(
        Guid userId,
        string connectionId)
    {
        bool isOffline = false;

        lock (_lock)
        {
            if (!_connections.TryGetValue(
                    userId,
                    out var userConnections))
            {
                return Task.FromResult(false);
            }

            userConnections.Remove(connectionId);

            if (userConnections.Count == 0)
            {
                _connections.TryRemove(
                    userId,
                    out _);

                isOffline = true;
            }
        }

        return Task.FromResult(isOffline);
    }

    public Task<bool> IsOnlineAsync(
        Guid userId)
    {
        return Task.FromResult(
            _connections.ContainsKey(userId));
    }

    public Task<IReadOnlyCollection<Guid>>
        GetOnlineUsersAsync()
    {
        IReadOnlyCollection<Guid> users =
            _connections.Keys.ToList();

        return Task.FromResult(users);
    }
}