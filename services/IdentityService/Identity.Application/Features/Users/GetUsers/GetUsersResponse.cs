namespace Identity.Application.Features.Users.GetUsers;

public sealed record GetUsersResponse(
    Guid Id,
    string UserName,
    string Email,
    string? ProfilePicture,
    bool IsOnline);