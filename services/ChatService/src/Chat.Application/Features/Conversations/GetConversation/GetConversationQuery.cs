using MediatR;

namespace Chat.Application.Features.Conversations.GetConversation;

public sealed record GetConversationQuery(Guid ConversationId)
    : IRequest<GetConversationResponse>;