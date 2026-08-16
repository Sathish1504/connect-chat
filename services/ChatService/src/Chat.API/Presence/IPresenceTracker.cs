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

    Task<IReadOnlyCollection<string>> GetConnectionIdsAsync(
        Guid userId);

    Task<IReadOnlyCollection<string>> GetChatConnectionIdsAsync(
    Guid userId);

    Task ChatConnectionAsync(
        Guid userId,
        string connectionId);

    Task<bool> ChatDisconnectedAsync(
        Guid userId,
        string connectionId);
}