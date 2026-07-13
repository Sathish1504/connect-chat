using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Authentication.VerifyEmail;

public sealed class VerifyEmailCommandHandler
    : IRequestHandler<VerifyEmailCommand, VerifyEmailResponse>
{
    private readonly IUserRepository _userRepository;

    public VerifyEmailCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<VerifyEmailResponse> Handle(
        VerifyEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailVerificationTokenAsync(
            request.Token,
            cancellationToken);

        if (user is null)
        {
            return new VerifyEmailResponse(
                "Invalid verification token.");
        }

        if (user.EmailConfirmed)
        {
            return new VerifyEmailResponse(
                "Email has already been verified.");
        }

        if (user.EmailVerificationTokenExpiryTime is null ||
            user.EmailVerificationTokenExpiryTime < DateTime.UtcNow)
        {
            return new VerifyEmailResponse(
                "Verification token has expired.");
        }

        user.ConfirmEmail();

        await _userRepository.SaveChangesAsync(cancellationToken);

        return new VerifyEmailResponse(
            "Email verified successfully.");
    }
}