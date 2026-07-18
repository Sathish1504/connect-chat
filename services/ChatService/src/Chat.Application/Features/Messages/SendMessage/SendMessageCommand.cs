using Chat.Domain.Enums;
using MediatR;

namespace Chat.Application.Features.Messages.SendMessage;

public sealed record SendMessageCommand(
    Guid ConversationId,
    Guid SenderId,
    string Content,
    MessageType Type)
    : IRequest<SendMessageResponse>;