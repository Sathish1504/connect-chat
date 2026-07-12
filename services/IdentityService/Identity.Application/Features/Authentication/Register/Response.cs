namespace Identity.Application.Features.Authentication.Register;

public sealed record Response(
    Guid UserId,
    string UserName,
    string Email
);