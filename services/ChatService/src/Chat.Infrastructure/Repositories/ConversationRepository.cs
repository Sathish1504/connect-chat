using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Conversation> AddAsync(
        Conversation conversation,
        CancellationToken cancellationToken)
    {
        await _context.Conversations.AddAsync(conversation, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return conversation;
    }

    public async Task<Conversation?> GetByIdAsync(
        Guid conversationId,
        CancellationToken cancellationToken)
    {
        return await _context.Conversations
            .Include(c => c.Participants)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(
                c => c.Id == conversationId,
                cancellationToken);
    }

    public async Task<bool> IsParticipantAsync(
    Guid conversationId,
    Guid userId,
    CancellationToken cancellationToken)
    {
        return await _context.ConversationParticipants
            .AnyAsync(
                x => x.ConversationId == conversationId &&
                     x.UserId == userId,
                cancellationToken);
    }
}