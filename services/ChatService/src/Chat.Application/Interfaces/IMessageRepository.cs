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
}