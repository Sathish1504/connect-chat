using System.Collections.Concurrent;

namespace Chat.API.Presence;

public sealed class InMemoryPresenceTracker
    : IPresenceTracker
{
    // PresenceHub connections
    private readonly ConcurrentDictionary<Guid, HashSet<string>>
        _connections = new();

    // ChatHub connections
    private readonly ConcurrentDictionary<Guid, HashSet<string>>
        _chatConnections = new();

    private readonly object _lock = new();

    // =========================================================
    // PresenceHub
    // =========================================================

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

                return Task.FromResult(true);
            }
        }

        return Task.FromResult(false);
    }

    public Task<bool> IsOnlineAsync(
        Guid userId)
    {
        lock (_lock)
        {
            return Task.FromResult(
                _connections.ContainsKey(userId));
        }
    }

    public Task<IReadOnlyCollection<Guid>>
        GetOnlineUsersAsync()
    {
        lock (_lock)
        {
            IReadOnlyCollection<Guid> users =
                _connections.Keys.ToList();

            return Task.FromResult(users);
        }
    }

    public Task<IReadOnlyCollection<string>>
        GetConnectionIdsAsync(
            Guid userId)
    {
        lock (_lock)
        {
            if (!_connections.TryGetValue(
                    userId,
                    out var connections))
            {
                return Task.FromResult<
                    IReadOnlyCollection<string>>(
                    Array.Empty<string>());
            }

            return Task.FromResult<
                IReadOnlyCollection<string>>(
                connections.ToArray());
        }
    }

    // =========================================================
    // ChatHub
    // =========================================================

    public Task ChatConnectionAsync(
        Guid userId,
        string connectionId)
    {
        lock (_lock)
        {
            if (!_chatConnections.TryGetValue(
                    userId,
                    out var connections))
            {
                connections = [];

                _chatConnections[userId] =
                    connections;
            }

            connections.Add(connectionId);
        }

        return Task.CompletedTask;
    }

    public Task<bool> ChatDisconnectedAsync(
        Guid userId,
        string connectionId)
    {
        lock (_lock)
        {
            if (!_chatConnections.TryGetValue(
                    userId,
                    out var connections))
            {
                return Task.FromResult(false);
            }

            connections.Remove(connectionId);

            if (connections.Count == 0)
            {
                _chatConnections.TryRemove(
                    userId,
                    out _);

                return Task.FromResult(true);
            }
        }

        return Task.FromResult(false);
    }

    public Task<IReadOnlyCollection<string>>
        GetChatConnectionIdsAsync(
            Guid userId)
    {
        lock (_lock)
        {
            if (!_chatConnections.TryGetValue(
                    userId,
                    out var connections))
            {
                return Task.FromResult<
                    IReadOnlyCollection<string>>(
                    Array.Empty<string>());
            }

            return Task.FromResult<
                IReadOnlyCollection<string>>(
                connections.ToArray());
        }
    }
}