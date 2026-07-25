using Chat.Domain.Entities;

namespace Chat.Application.Interfaces;

public interface IMessageRepository
{
    Task<Message> AddAsync(
        Message message,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Message>> GetByConversationIdAsync(
        Guid conversationId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Guid>> MarkConversationDeliveredAsync(
    Guid conversationId,
    Guid currentUserId,
    CancellationToken cancellationToken);

    Task<IReadOnlyList<Guid>> MarkConversationReadAsync(
    Guid conversationId,
    Guid currentUserId,
    CancellationToken cancellationToken);
}