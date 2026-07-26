using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Enums;
using MediatR;

namespace Chat.Application.Features.Conversations.CreateConversation;

public sealed class CreateConversationHandler
    : IRequestHandler<CreateConversationCommand, CreateConversationResponse>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly ICurrentUserService _currentUser;

    public CreateConversationHandler(
        IConversationRepository conversationRepository,
        ICurrentUserService currentUser)
    {
        _conversationRepository = conversationRepository;
        _currentUser = currentUser;
    }

    public async Task<CreateConversationResponse> Handle(
        CreateConversationCommand request,
        CancellationToken cancellationToken)
    {
        // Prevent duplicate direct conversations
        if (request.Type == ConversationType.Direct)
        {
            if (request.ParticipantIds.Count != 1)
            {
                throw new InvalidOperationException(
                    "Direct conversation must contain exactly one participant.");
            }

            var otherUserId = request.ParticipantIds[0];

            var existingConversation =
                await _conversationRepository.GetDirectConversationAsync(
                    _currentUser.UserId,
                    otherUserId,
                    cancellationToken);

            if (existingConversation is not null)
            {
                return new CreateConversationResponse(
                    existingConversation.Id,
                    "Conversation already exists.");
            }
        }

        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = _currentUser.UserId
        };

        var participants = request.ParticipantIds
            .Append(_currentUser.UserId)
            .Distinct();

        foreach (var participantId in participants)
        {
            conversation.Participants.Add(new ConversationParticipant
            {
                Id = Guid.NewGuid(),
                ConversationId = conversation.Id,
                UserId = participantId,
                JoinedAt = DateTime.UtcNow
            });
        }

        await _conversationRepository.AddAsync(
            conversation,
            cancellationToken);

        return new CreateConversationResponse(
            conversation.Id,
            "Conversation created successfully.");
    }
}