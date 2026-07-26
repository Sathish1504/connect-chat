using Chat.Application.Features.Messages.GetConversationMessages;
using Chat.Application.Features.Messages.MarkConversationRead;
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
            .RequireAuthorization()
            .WithName("SendMessage")
            .WithTags("Messages");

        app.MapGet("/api/conversations/{conversationId:guid}/messages",
            async (
                Guid conversationId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetConversationMessagesQuery(conversationId),
                    cancellationToken);

                return Results.Ok(result);
            })
            .WithName("GetConversationMessages")
            .WithTags("Messages");


            app.MapPost(
            "/api/conversations/{conversationId:guid}/read",
            async (
                Guid conversationId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new MarkConversationReadCommand(conversationId),
                    cancellationToken);

                return Results.Ok(result);
            })
            .RequireAuthorization()
            .WithName("MarkConversationRead")
            .WithTags("Messages");

        return app;
    }
}