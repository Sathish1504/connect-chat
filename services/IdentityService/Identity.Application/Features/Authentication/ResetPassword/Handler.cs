using Identity.Application.Interfaces;
using Identity.Application.Interfaces.Security;
using MediatR;

namespace Identity.Application.Features.Authentication.ResetPassword;

public sealed class Handler : IRequestHandler<Command, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public Handler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByPasswordResetTokenAsync(
            request.Token,
            cancellationToken);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid password reset token.");
        }

        if (user.PasswordResetTokenExpiryTime is null ||
            user.PasswordResetTokenExpiryTime < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Password reset token has expired.");
        }

        var passwordHash = _passwordHasher.HashPassword(
            request.NewPassword);

        user.ChangePassword(passwordHash);

        // Security: revoke any existing refresh token
        user.RevokeRefreshToken();

        await _userRepository.SaveChangesAsync(cancellationToken);

        return new Response("Password has been reset successfully.");
    }
}