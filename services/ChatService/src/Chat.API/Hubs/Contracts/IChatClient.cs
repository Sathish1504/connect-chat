namespace Chat.API.Hubs.Contracts;

public interface IChatClient
{
    Task ReceiveMessage(object message);

    Task UserTyping(UserTypingDto message);

    Task UserStoppedTyping(object message);

    Task MessageDelivered(MessageDeliveredDto message);

    Task MessageRead(MessageReadDto message);

    Task IncomingCall(object call);
}