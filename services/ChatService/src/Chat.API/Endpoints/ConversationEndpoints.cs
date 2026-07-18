using Chat.Application.Features.Conversations.CreateConversation;
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

        return app;
    }
}