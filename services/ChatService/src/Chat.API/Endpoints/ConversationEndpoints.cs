using Chat.Application.Features.Conversations.CreateConversation;
using Chat.Application.Features.Conversations.GetConversations;
using MediatR;

namespace Chat.API.Endpoints;

public static class ConversationEndpoints
{
    public static IEndpointRouteBuilder MapConversationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/conversations",
            async (
                CreateConversationCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(command, cancellationToken);

                return Results.Created(
                    $"/api/conversations/{result.ConversationId}",
                    result);
            })
            .WithName("CreateConversation")
            .WithTags("Conversations");

        app.MapGet("/api/conversations",
    async (
        ISender sender,
        CancellationToken cancellationToken) =>
    {
        var result = await sender.Send(
            new GetConversationsQuery(),
            cancellationToken);

        return Results.Ok(result);
    })
    .RequireAuthorization()
    .WithName("GetConversations")
    .WithTags("Conversations");

        return app;
    }
}