using MediatR;

namespace Chat.Application.Features.Messages.GetConversationMessages;

public sealed record GetConversationMessagesQuery(
    Guid ConversationId)
    : IRequest<List<GetConversationMessagesResponse>>;