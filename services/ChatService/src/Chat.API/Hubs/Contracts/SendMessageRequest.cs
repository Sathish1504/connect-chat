using Chat.Domain.Enums;

namespace Chat.API.Hubs.Contracts;

public sealed record SendMessageRequest(
    Guid ConversationId,
    //Guid SenderId,
    string Content,
    MessageType Type);