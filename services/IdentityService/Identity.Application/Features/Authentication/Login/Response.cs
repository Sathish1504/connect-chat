namespace Identity.Application.Features.Authentication.Login;

public sealed record Response(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt);