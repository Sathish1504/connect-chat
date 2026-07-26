using Chat.API.Hubs.Contracts;
using Chat.Application.Features.Messages.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

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

    public async Task StartTyping(Guid conversationId)
    {
        var userId = Guid.Parse(
            Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var userName =
            Context.User?.Identity?.Name
            ?? Context.User?.FindFirst(ClaimTypes.Email)?.Value
            ?? "Unknown";

        await Clients
            .OthersInGroup(HubGroups.Conversation(conversationId))
            .SendAsync(
                "UserTyping",
                new UserTypingDto
                {
                    ConversationId = conversationId,
                    UserId = userId,
                    UserName = userName
                });
    }

    public async Task StopTyping(Guid conversationId)
    {
        var userId = Guid.Parse(
            Context.User!.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        await Clients
            .OthersInGroup(HubGroups.Conversation(conversationId))
            .SendAsync(
                "UserStoppedTyping",
                new
                {
                    ConversationId = conversationId,
                    UserId = userId
                });
    }

    public async Task<SendMessageResponse> SendMessageRealtime(
    SendMessageRequest request)
    {
        var senderId = Guid.Parse(
            Context.User!.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

        var command = new SendMessageCommand(
            request.ConversationId,
            request.Content,
            request.Type);

        var response = await _mediator.Send(command);

        await Clients
            .Group(HubGroups.Conversation(request.ConversationId))
            .SendAsync("ReceiveMessage", new
            {
                response.MessageId,
                ConversationId = request.ConversationId,
                SenderId = senderId,
                request.Content,
                request.Type,
                Status = (int)response.Status
            });

        return response;
    }

}