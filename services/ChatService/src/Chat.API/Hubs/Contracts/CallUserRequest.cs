namespace Chat.API.Hubs.Contracts;

public sealed record CallUserRequest(
    Guid TargetUserId,
    Guid ConversationId,
    string CallType);