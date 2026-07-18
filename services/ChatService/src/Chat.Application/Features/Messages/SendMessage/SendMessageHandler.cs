using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Domain.Enums;
using MediatR;

namespace Chat.Application.Features.Messages.SendMessage;

public sealed class SendMessageHandler
    : IRequestHandler<SendMessageCommand, SendMessageResponse>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;

    public SendMessageHandler(
        IConversationRepository conversationRepository,
        IMessageRepository messageRepository)
    {
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
    }

    public async Task<SendMessageResponse> Handle(
        SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(
            request.ConversationId,
            cancellationToken);

        if (conversation is null)
            throw new InvalidOperationException("Conversation not found.");

        var isParticipant = await _conversationRepository.IsParticipantAsync(
            request.ConversationId,
            request.SenderId,
            cancellationToken);

        if (!isParticipant)
            throw new InvalidOperationException("Sender is not a participant of this conversation.");

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = request.ConversationId,
            SenderId = request.SenderId,
            Content = request.Content,
            Type = request.Type,
            Status = MessageStatus.Sent,
            CreatedAt = DateTime.UtcNow
        };

        await _messageRepository.AddAsync(message, cancellationToken);

        return new SendMessageResponse(
            message.Id,
            message.Status.ToString());
    }
}