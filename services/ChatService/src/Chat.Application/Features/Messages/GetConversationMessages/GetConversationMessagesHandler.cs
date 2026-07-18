using Chat.Application.Interfaces;
using MediatR;

namespace Chat.Application.Features.Messages.GetConversationMessages;

public sealed class GetConversationMessagesHandler
    : IRequestHandler<GetConversationMessagesQuery, List<GetConversationMessagesResponse>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IConversationRepository _conversationRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetConversationMessagesHandler(
        IMessageRepository messageRepository,
        IConversationRepository conversationRepository,
        ICurrentUserService currentUserService)
    {
        _messageRepository = messageRepository;
        _conversationRepository = conversationRepository;
        _currentUserService = currentUserService;
    }

    public async Task<List<GetConversationMessagesResponse>> Handle(
        GetConversationMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var isParticipant = await _conversationRepository.IsParticipantAsync(
            request.ConversationId,
            userId,
            cancellationToken);

        if (!isParticipant)
        {
            throw new UnauthorizedAccessException(
                "You are not a participant of this conversation.");
        }

        var messages = await _messageRepository.GetByConversationIdAsync(
            request.ConversationId,
            cancellationToken);

        return messages
            .Select(m => new GetConversationMessagesResponse(
                m.Id,
                m.SenderId,
                m.Content,
                m.Type,
                m.Status,
                m.CreatedAt))
            .ToList();
    }
}