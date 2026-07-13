using System.Security.Cryptography;
using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Authentication.SendVerificationEmail;

public sealed class SendVerificationEmailCommandHandler
    : IRequestHandler<
        SendVerificationEmailCommand,
        SendVerificationEmailResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public SendVerificationEmailCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<SendVerificationEmailResponse> Handle(
        SendVerificationEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(
            request.Email,
            cancellationToken);

        // Never reveal whether the email exists.
        if (user is null)
        {
            return new SendVerificationEmailResponse(
                "If the account exists, a verification email has been sent.");
        }

        // Already verified.
        if (user.EmailConfirmed)
        {
            return new SendVerificationEmailResponse(
                "If the account exists, a verification email has been sent.");
        }

        var token = Convert.ToHexString(
            RandomNumberGenerator.GetBytes(32));

        var expiry = DateTime.UtcNow.AddHours(24);

        user.SetEmailVerificationToken(token, expiry);

        await _userRepository.SaveChangesAsync(cancellationToken);

        var verificationUrl =
            $"https://localhost:5176/api/auth/verify-email?token={token}";

        var body =
$"""
Hello {user.UserName},

Please verify your email by clicking the link below.

{verificationUrl}

This link expires in 24 hours.

Regards,
ConnectChat Team
""";

        await _emailService.SendEmailAsync(
            user.Email,
            "Verify your Email",
            body,
            cancellationToken);

        return new SendVerificationEmailResponse(
            "If the account exists, a verification email has been sent.");
    }
}