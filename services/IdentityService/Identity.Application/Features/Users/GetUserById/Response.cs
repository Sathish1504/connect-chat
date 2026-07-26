namespace Identity.Application.Features.Users.GetUserById;

public sealed record Response(
    Guid Id,
    string UserName,
    string Email,
    string? ProfilePicture,
    bool IsOnline);