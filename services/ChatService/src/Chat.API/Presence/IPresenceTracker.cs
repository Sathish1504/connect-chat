namespace Chat.API.Presence;

public interface IPresenceTracker
{
    Task UserConnectedAsync(
        Guid userId,
        string connectionId);

    Task<bool> UserDisconnectedAsync(
        Guid userId,
        string connectionId);

    Task<bool> IsOnlineAsync(
        Guid userId);

    Task<IReadOnlyCollection<Guid>> GetOnlineUsersAsync();
}