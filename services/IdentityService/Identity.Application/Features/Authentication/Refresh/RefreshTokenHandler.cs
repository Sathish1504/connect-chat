using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Authentication.Refresh;

public sealed class RefreshTokenHandler
    : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RefreshTokenHandler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<RefreshTokenResponse> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByRefreshTokenAsync(
            request.RefreshToken,
            cancellationToken);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid refresh token.");
        }

        if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token has expired.");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("User account is inactive.");
        }

        var accessToken =
            _jwtTokenGenerator.GenerateAccessToken(user);

        var newRefreshToken =
            _jwtTokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(
            newRefreshToken,
            DateTime.UtcNow.AddDays(7));

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return new RefreshTokenResponse(
            accessToken.Token,
            newRefreshToken,
            accessToken.ExpiresAt);
    }
}