using Chat.Domain.Enums;

namespace Chat.Domain.Entities;

public class Conversation
{
    public Guid Id { get; set; }

    public ConversationType Type { get; set; }

    public string? Name { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<ConversationParticipant> Participants { get; set; } = new List<ConversationParticipant>();

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}