namespace Identity.Application.Features.Authentication.Register;

public sealed record RegisterResponse(
    Guid UserId,
    string UserName,
    string Email
);