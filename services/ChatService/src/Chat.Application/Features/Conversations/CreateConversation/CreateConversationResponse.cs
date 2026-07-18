namespace Chat.Application.Features.Conversations.CreateConversation;

public sealed record CreateConversationResponse(
    Guid ConversationId,
    string Message);