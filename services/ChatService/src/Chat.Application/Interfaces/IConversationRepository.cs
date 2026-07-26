using Chat.Domain.Entities;
using Chat.Application.Features.Conversations.GetConversations;

namespace Chat.Application.Interfaces;

public interface IConversationRepository
{
    Task<Conversation> AddAsync(
        Conversation conversation,
        CancellationToken cancellationToken);

    Task<Conversation?> GetByIdAsync(
        Guid conversationId,
        CancellationToken cancellationToken);

    Task<bool> IsParticipantAsync(
        Guid conversationId,
        Guid userId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ConversationSummaryResponse>>
    GetUserConversationsAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<Conversation?> GetDirectConversationAsync(
    Guid user1Id,
    Guid user2Id,
    CancellationToken cancellationToken);
}