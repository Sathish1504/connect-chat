using MediatR;

namespace Identity.Application.Features.Authentication.Refresh;

public sealed record RefreshTokenCommand(
    string RefreshToken
) : IRequest<RefreshTokenResponse>;