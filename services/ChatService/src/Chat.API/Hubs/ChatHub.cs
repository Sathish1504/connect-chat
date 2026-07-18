using Chat.API.Hubs.Contracts;
using Chat.Application.Features.Messages.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Hubs;

[Authorize]
public sealed class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Connected: {Context.UserIdentifier}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Disconnected: {Context.UserIdentifier}");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task JoinConversation(Guid conversationId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            HubGroups.Conversation(conversationId));
    }

    public async Task LeaveConversation(Guid conversationId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            HubGroups.Conversation(conversationId));
    }

    public async Task<SendMessageResponse> SendMessageRealtime(
        SendMessageRequest request)
    {
        var command = new SendMessageCommand(
            request.ConversationId,
            request.SenderId,
            request.Content,
            request.Type);

        var response = await _mediator.Send(command);

        await Clients
            .Group(HubGroups.Conversation(request.ConversationId))
            .SendAsync("ReceiveMessage", new
            {
                response.MessageId,
                request.ConversationId,
                request.SenderId,
                request.Content,
                request.Type,
                Status = response.Status
            });

        return response;
    }
}