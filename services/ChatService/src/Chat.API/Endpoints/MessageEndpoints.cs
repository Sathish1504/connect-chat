using Chat.Application.Features.Messages.SendMessage;
using MediatR;

namespace Chat.API.Endpoints;

public static class MessageEndpoints
{
    public static IEndpointRouteBuilder MapMessageEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/messages",
            async (
                SendMessageCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(command, cancellationToken);

                return Results.Created(
                    $"/api/messages/{result.MessageId}",
                    result);
            })
            .WithName("SendMessage")
            .WithTags("Messages");

        return app;
    }
}