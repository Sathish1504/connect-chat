namespace Chat.API.Hubs.Contracts;

public sealed class MessageDeliveredDto
{
    public Guid ConversationId { get; init; }

    public Guid MessageId { get; init; }

    public int Status { get; init; }
}