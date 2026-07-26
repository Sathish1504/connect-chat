namespace Chat.API.Hubs.Contracts;

public sealed class MessageReadDto
{
    public Guid ConversationId { get; init; }

    public Guid MessageId { get; init; }

    public int Status { get; init; }
}