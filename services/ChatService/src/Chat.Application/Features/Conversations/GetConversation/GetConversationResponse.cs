public sealed record ParticipantDto(Guid UserId);

public sealed record MessageDto(
    Guid Id,
    Guid SenderId,
    string Content,
    DateTime CreatedAt);

public sealed record GetConversationResponse(
    Guid Id,
    string Type,
    string? Name,
    IReadOnlyList<ParticipantDto> Participants,
    IReadOnlyList<MessageDto> Messages);