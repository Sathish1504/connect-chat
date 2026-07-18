using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using MediatR;

namespace Chat.Application.Features.Conversations.CreateConversation;

public sealed class CreateConversationHandler
    : IRequestHandler<CreateConversationCommand, CreateConversationResponse>
{
    private readonly IConversationRepository _conversationRepository;

    public CreateConversationHandler(
        IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository;
    }

    public async Task<CreateConversationResponse> Handle(
        CreateConversationCommand request,
        CancellationToken cancellationToken)
    {
        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            Type = request.Type,
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,

            // Later we'll replace this with the authenticated user's ID
            CreatedBy = request.ParticipantIds.First()
        };

        foreach (var participantId in request.ParticipantIds.Distinct())
        {
            conversation.Participants.Add(new ConversationParticipant
            {
                Id = Guid.NewGuid(),
                ConversationId = conversation.Id,
                UserId = participantId,
                JoinedAt = DateTime.UtcNow
            });
        }

        await _conversationRepository.AddAsync(conversation, cancellationToken);

        return new CreateConversationResponse(
            conversation.Id,
            "Conversation created successfully.");
    }
}