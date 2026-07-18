using Chat.Domain.Enums;

namespace Chat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }

    public Guid ConversationId { get; set; }

    public Guid SenderId { get; set; }

    public string Content { get; set; } = string.Empty;

    public MessageType Type { get; set; }

    public MessageStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    public Conversation Conversation { get; set; } = null!;
}