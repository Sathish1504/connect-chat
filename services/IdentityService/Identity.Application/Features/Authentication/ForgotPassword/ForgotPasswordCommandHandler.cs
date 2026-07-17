using System.Security.Cryptography;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Authentication.ForgotPassword;

public sealed class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<ForgotPasswordResponse> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        // Prevent account enumeration.
        if (user is null)
        {
            return new ForgotPasswordResponse(
                "If the account exists, a password reset email has been sent.");
        }

        var token = Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32));

        var expiry = DateTime.UtcNow.AddHours(24);

        user.SetPasswordResetToken(token, expiry);

        await _userRepository.SaveChangesAsync(cancellationToken);

        var resetUrl =
            $"https://localhost:5176/api/auth/reset-password?token={token}";

        var body =
$"""
Hello {user.UserName},

A request was received to reset your password.

Use the link below to continue.

{resetUrl}

This link expires in 24 hours.

If you did not request this password reset, you can safely ignore this email.

Regards,
ConnectChat Team
""";

        await _emailService.SendEmailAsync(
            user.Email,
            "Reset your password",
            body,
            cancellationToken);

        return new ForgotPasswordResponse(
            "If the account exists, a password reset email has been sent.");
    }
}