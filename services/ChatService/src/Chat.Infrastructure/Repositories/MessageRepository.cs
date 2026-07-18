using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Infrastructure.Persistence;

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
        await _context.Messages.AddAsync(message, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return message;
    }
}