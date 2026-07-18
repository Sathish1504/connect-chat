using Chat.Domain.Enums;

namespace Chat.Application.Features.Messages.GetConversationMessages;

public sealed record GetConversationMessagesResponse(
    Guid Id,
    Guid SenderId,
    string Content,
    MessageType Type,
    MessageStatus Status,
    DateTime CreatedAt);