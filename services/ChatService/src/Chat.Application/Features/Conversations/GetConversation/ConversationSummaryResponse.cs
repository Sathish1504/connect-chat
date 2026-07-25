using Chat.Domain.Enums;

namespace Chat.Application.Features.Conversations.GetConversations;

public sealed record ConversationSummaryResponse(
    Guid Id,
    string Name,
    ConversationType Type,
    Guid OtherParticipantId,
    string? LastMessage,
    DateTime? LastMessageAt);