using Chat.Application.Interfaces;
using MediatR;

namespace Chat.Application.Features.Messages.MarkConversationDelivered;

public sealed class MarkConversationDeliveredHandler
    : IRequestHandler<
        MarkConversationDeliveredCommand,
        MarkConversationDeliveredResponse>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IChatNotificationService _notificationService;

    public MarkConversationDeliveredHandler(
        IMessageRepository messageRepository,
        ICurrentUserService currentUser,
        IChatNotificationService notificationService)
    {
        _messageRepository = messageRepository;
        _currentUser = currentUser;
        _notificationService = notificationService;
    }

    public async Task<MarkConversationDeliveredResponse> Handle(
        MarkConversationDeliveredCommand request,
        CancellationToken cancellationToken)
    {
        var updatedMessageIds =
         await _messageRepository
        .MarkConversationDeliveredAsync(
            request.ConversationId,
            _currentUser.UserId,
            cancellationToken);

        foreach (var messageId in updatedMessageIds)
        {
            await _notificationService
                .NotifyMessageDeliveredAsync(
                    request.ConversationId,
                    messageId);
        }

        return new MarkConversationDeliveredResponse(
            updatedMessageIds.Count);
    }
}