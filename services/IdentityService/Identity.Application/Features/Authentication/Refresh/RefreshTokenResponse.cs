namespace Identity.Application.Features.Authentication.Refresh;

public sealed record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);