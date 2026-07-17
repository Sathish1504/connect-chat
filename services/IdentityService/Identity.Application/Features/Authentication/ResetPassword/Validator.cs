using FluentValidation;

namespace Identity.Application.Features.Authentication.ResetPassword;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Token)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match.");
    }
}