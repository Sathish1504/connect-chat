namespace Chat.Application.Features.Messages.SendMessage;

public sealed record SendMessageResponse(
    Guid MessageId,
    string Status);