using Chat.Domain.Entities;

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
}