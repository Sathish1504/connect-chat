namespace Chat.API.Hubs.Contracts;

public sealed class UserTypingDto
{
    public Guid ConversationId { get; init; }

    public Guid UserId { get; init; }

    public string UserName { get; init; } = string.Empty;
}