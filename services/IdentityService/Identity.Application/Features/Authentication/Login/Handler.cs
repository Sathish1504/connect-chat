using MediatR;
using Identity.Application.Interfaces;
using Identity.Application.Interfaces.Security;

namespace Identity.Application.Features.Authentication.Login;

public sealed class Handler : IRequestHandler<Command, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public Handler(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        if (!_passwordHasher.VerifyPassword(
            request.Password,
            user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var accessToken =
            _jwtTokenGenerator.GenerateAccessToken(user);

        var refreshToken =
            _jwtTokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(
            refreshToken,
            DateTime.UtcNow.AddDays(7));

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return new Response(
            accessToken.Token,
            refreshToken,
            accessToken.ExpiresAt);
    }
}