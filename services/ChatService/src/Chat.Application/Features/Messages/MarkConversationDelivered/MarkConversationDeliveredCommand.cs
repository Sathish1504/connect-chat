using MediatR;

namespace Chat.Application.Features.Messages.MarkConversationDelivered;

public sealed record MarkConversationDeliveredCommand(
    Guid ConversationId)
    : IRequest<MarkConversationDeliveredResponse>;