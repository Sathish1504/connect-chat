using Chat.API.Hubs.Contracts;
using Chat.Application.Features.Messages.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Chat.API.Presence;

namespace Chat.API.Hubs;

[Authorize]
public sealed class ChatHub : Hub
{
    private readonly IMediator _mediator;
    private readonly IPresenceTracker _presenceTracker;

    public ChatHub(
    IMediator mediator,
    IPresenceTracker presenceTracker)
    {
        _mediator = mediator;
        _presenceTracker = presenceTracker;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Guid.Parse(
            Context.User!
                .FindFirst(ClaimTypes.NameIdentifier)!
                .Value);

        await _presenceTracker.ChatConnectionAsync(
            userId,
            Context.ConnectionId);

        Console.WriteLine(
            $"Chat connected: {userId} / {Context.ConnectionId}");

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(
    Exception? exception)
    {
        var userId = Guid.Parse(
            Context.User!
                .FindFirst(ClaimTypes.NameIdentifier)!
                .Value);

        await _presenceTracker.ChatDisconnectedAsync(
            userId,
            Context.ConnectionId);

        Console.WriteLine(
            $"Chat disconnected: {userId} / {Context.ConnectionId}");

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

    public async Task CallUser(
    CallUserRequest request)
    {
        var callerId = Guid.Parse(
            Context.User!
                .FindFirst(ClaimTypes.NameIdentifier)!
                .Value);

        var connectionIds =
            await _presenceTracker.GetChatConnectionIdsAsync(
                request.TargetUserId);

        if (connectionIds.Count == 0)
        {
            Console.WriteLine(
                $"No ChatHub connection found for " +
                $"{request.TargetUserId}");

            return;
        }

        Console.WriteLine(
            $"Calling {request.TargetUserId} " +
            $"through {connectionIds.Count} ChatHub connection(s)");

        await Clients
            .Clients(connectionIds)
            .SendAsync(
                "IncomingCall",
                new
                {
                    CallerId = callerId,
                    TargetUserId = request.TargetUserId,
                    ConversationId = request.ConversationId,
                    CallType = request.CallType
                });
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