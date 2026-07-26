namespace Chat.Application.Interfaces;

public interface IChatNotificationService
{
    Task NotifyMessageDeliveredAsync(
    Guid conversationId,
    Guid messageId);

    Task NotifyMessageReadAsync(
    Guid conversationId,
    Guid messageId);
}