using MediatR;

namespace Chat.Application.Features.Conversations.GetConversations;

public sealed record GetConversationsQuery()
    : IRequest<IReadOnlyList<ConversationSummaryResponse>>;