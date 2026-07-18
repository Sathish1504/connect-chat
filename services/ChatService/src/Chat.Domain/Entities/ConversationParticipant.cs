namespace Chat.Domain.Entities;

public class ConversationParticipant
{
    public Guid Id { get; set; }

    public Guid ConversationId { get; set; }

    public Guid UserId { get; set; }

    public DateTime JoinedAt { get; set; }

    public Conversation Conversation { get; set; } = null!;
}