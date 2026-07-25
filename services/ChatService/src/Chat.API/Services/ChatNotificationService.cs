using Chat.API.Hubs;
using Chat.API.Hubs.Contracts;
using Microsoft.AspNetCore.SignalR;
using Chat.Application.Interfaces;

namespace Chat.API.Services;

public sealed class ChatNotificationService
    : IChatNotificationService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public ChatNotificationService(
        IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyMessageDeliveredAsync(
        Guid conversationId,
        Guid messageId)
    {
        await _hubContext.Clients
            .Group(HubGroups.Conversation(conversationId))
            .SendAsync(
                "MessageDelivered",
                new MessageDeliveredDto
                {
                    ConversationId = conversationId,
                    MessageId = messageId,
                    Status = 2
                });
    }

    public async Task NotifyMessageReadAsync(
    Guid conversationId,
    Guid messageId)
    {
        await _hubContext.Clients
            .Group(HubGroups.Conversation(conversationId))
            .SendAsync(
                "MessageRead",
                new MessageReadDto
                {
                    ConversationId = conversationId,
                    MessageId = messageId,
                    Status = 3
                });
    }
}