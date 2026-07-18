using Chat.Domain.Entities;

namespace Chat.Application.Interfaces;

public interface IConversationRepository
{
    Task<Conversation> AddAsync(Conversation conversation, CancellationToken cancellationToken);
}