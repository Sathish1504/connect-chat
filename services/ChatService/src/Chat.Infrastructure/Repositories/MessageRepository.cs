using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Enums;
using Chat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Message> AddAsync(
        Message message,
        CancellationToken cancellationToken)
    {
        await _context.Messages.AddAsync(
            message,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        return message;
    }

    public async Task<IReadOnlyList<Message>>
        GetByConversationIdAsync(
        Guid conversationId,
        CancellationToken cancellationToken)
    {
        return await _context.Messages
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>>
MarkConversationDeliveredAsync(
    Guid conversationId,
    Guid currentUserId,
    CancellationToken cancellationToken)
    {
        var messages = await _context.Messages

            .Where(m =>
                m.ConversationId == conversationId &&
                m.SenderId != currentUserId &&
                m.Status == MessageStatus.Sent)

            .ToListAsync(cancellationToken);

        if (messages.Count == 0)
            return [];

        foreach (var message in messages)
        {
            message.Status = MessageStatus.Delivered;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return messages
            .Select(x => x.Id)
            .ToList();
    }

    public async Task<IReadOnlyList<Guid>>
    MarkConversationReadAsync(
    Guid conversationId,
    Guid currentUserId,
    CancellationToken cancellationToken)
    {
        var messages = await _context.Messages

            .Where(m =>
                m.ConversationId == conversationId &&
                m.SenderId != currentUserId &&
                m.Status != MessageStatus.Read)

            .ToListAsync(cancellationToken);

        if (messages.Count == 0)
            return [];

        foreach (var message in messages)
        {
            message.Status = MessageStatus.Read;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return messages
            .Select(m => m.Id)
            .ToList();
    }
}