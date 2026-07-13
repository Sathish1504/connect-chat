namespace Identity.Application.Features.Users.GetProfile;

public sealed record Response(
    Guid Id,
    string UserName,
    string Email,
    string? ProfilePicture,
    bool EmailConfirmed,
    bool IsOnline,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? LastSeenAt);