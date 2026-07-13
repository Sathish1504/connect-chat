using FluentValidation;

namespace Identity.Application.Features.Authentication.Refresh;

public sealed class RefreshTokenValidator
    : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .WithMessage("Refresh token is required.");
    }
}