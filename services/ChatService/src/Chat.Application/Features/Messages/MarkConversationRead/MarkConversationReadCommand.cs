using MediatR;

namespace Chat.Application.Features.Messages.MarkConversationRead;

public sealed record MarkConversationReadCommand(
    Guid ConversationId)
    : IRequest<MarkConversationReadResponse>;