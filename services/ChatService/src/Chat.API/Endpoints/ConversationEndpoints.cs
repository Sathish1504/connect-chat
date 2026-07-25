using Chat.Application.Features.Conversations.CreateConversation;
using Chat.Application.Features.Conversations.GetConversations;
using Chat.Application.Features.Messages.MarkConversationDelivered;
using MediatR;

namespace Chat.API.Endpoints;

public static class ConversationEndpoints
{
    public static IEndpointRouteBuilder MapConversationEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/conversations")
            .RequireAuthorization()
            .WithTags("Conversations");

        // Create Conversation
        group.MapPost("/",
            async (
                CreateConversationCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    command,
                    cancellationToken);

                return Results.Created(
                    $"/api/conversations/{result.ConversationId}",
                    result);
            });

        // Get Conversations
        group.MapGet("/",
            async (
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetConversationsQuery(),
                    cancellationToken);

                return Results.Ok(result);
            });

        // Mark Delivered
        group.MapPost(
            "/{conversationId:guid}/delivered",
            async (
                Guid conversationId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new MarkConversationDeliveredCommand(
                        conversationId),
                    cancellationToken);

                return Results.Ok(result);
            });

        return app;
    }
}