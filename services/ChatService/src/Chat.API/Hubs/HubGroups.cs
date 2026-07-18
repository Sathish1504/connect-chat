namespace Chat.API.Hubs;

public static class HubGroups
{
    public static string Conversation(Guid conversationId)
        => $"conversation:{conversationId}";
}