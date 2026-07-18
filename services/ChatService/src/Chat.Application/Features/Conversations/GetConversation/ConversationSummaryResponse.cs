using Chat.Domain.Enums;

namespace Chat.Application.Features.Conversations.GetConversations;

public sealed record ConversationSummaryResponse(
    Guid Id,
    string Name,
    ConversationType Type,
    string? LastMessage,
    DateTime? LastMessageAt);