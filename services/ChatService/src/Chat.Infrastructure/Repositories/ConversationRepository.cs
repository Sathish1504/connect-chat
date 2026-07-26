using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Chat.Application.Features.Conversations.GetConversations;

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

    public async Task<IReadOnlyList<ConversationSummaryResponse>>
    GetUserConversationsAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.ConversationParticipants
            .Where(cp => cp.UserId == userId)
            .Select(cp => cp.Conversation)
            .Select(c => new ConversationSummaryResponse(
                                  c.Id,
                                  c.Name ?? "Direct Chat",
                                  c.Type,

                                  c.Participants
                                      .Where(p => p.UserId != userId)
                                      .Select(p => p.UserId)
                                      .FirstOrDefault(),

                                  c.Messages
                                      .OrderByDescending(m => m.CreatedAt)
                                      .Select(m => m.Content)
                                      .FirstOrDefault(),

                                  c.Messages
                                      .OrderByDescending(m => m.CreatedAt)
                                      .Select(m => (DateTime?)m.CreatedAt)
                                      .FirstOrDefault()))
            .ToListAsync(cancellationToken);
    }

    public async Task<Conversation?> GetDirectConversationAsync(
    Guid user1Id,
    Guid user2Id,
    CancellationToken cancellationToken)
    {
        return await _context.Conversations
            .Include(c => c.Participants)
            .Where(c => c.Type == Chat.Domain.Enums.ConversationType.Direct)
            .FirstOrDefaultAsync(c =>
                c.Participants.Count == 2 &&
                c.Participants.Any(p => p.UserId == user1Id) &&
                c.Participants.Any(p => p.UserId == user2Id),
                cancellationToken);
    }
}