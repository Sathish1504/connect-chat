using Chat.Application.Interfaces;
using MediatR;

namespace Chat.Application.Features.Messages.MarkConversationRead;

public sealed class MarkConversationReadHandler
    : IRequestHandler<
        MarkConversationReadCommand,
        MarkConversationReadResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IChatNotificationService _notificationService;

    public MarkConversationReadHandler(
        IMessageRepository messageRepository,
        ICurrentUserService currentUser,
        IChatNotificationService notificationService)
    {
        _messageRepository = messageRepository;
        _currentUser = currentUser;
        _notificationService = notificationService;
    }

    public async Task<MarkConversationReadResponse> Handle(
        MarkConversationReadCommand request,
        CancellationToken cancellationToken)
    {
        var updatedMessageIds =
            await _messageRepository
                .MarkConversationReadAsync(
                    request.ConversationId,
                    _currentUser.UserId,
                    cancellationToken);

        foreach (var messageId in updatedMessageIds)
        {
            await _notificationService
                .NotifyMessageReadAsync(
                    request.ConversationId,
                    messageId);
        }

        return new MarkConversationReadResponse(
            updatedMessageIds.Count);
    }
}