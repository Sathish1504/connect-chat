using Chat.Domain.Enums;
using MediatR;

namespace Chat.Application.Features.Conversations.CreateConversation;

public sealed record CreateConversationCommand(
    ConversationType Type,
    string? Name,
    List<Guid> ParticipantIds)
    : IRequest<CreateConversationResponse>;