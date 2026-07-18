using Chat.Application.Interfaces;
using MediatR;

namespace Chat.Application.Features.Messages.GetConversationMessages;

public class GetConversationMessagesHandler
    : IRequestHandler<GetConversationMessagesQuery, List<GetConversationMessagesResponse>>
{
    private readonly IMessageRepository _messageRepository;

    public GetConversationMessagesHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<GetConversationMessagesResponse>> Handle(
        GetConversationMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var messages = await _messageRepository.GetByConversationIdAsync(
            request.ConversationId,
            cancellationToken);

        return messages.Select(m => new GetConversationMessagesResponse(
            m.Id,
            m.SenderId,
            m.Content,
            m.Type,
            m.Status,
            m.CreatedAt))
        .ToList();
    }
}