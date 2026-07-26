using Chat.Domain.Enums;

namespace Chat.Application.Features.Messages.SendMessage;

public sealed record SendMessageResponse(
    Guid MessageId,
    MessageStatus Status);