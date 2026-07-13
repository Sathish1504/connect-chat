using FluentValidation;

namespace Identity.Application.Features.Authentication.SendVerificationEmail;

public sealed class SendVerificationEmailCommandValidator
    : AbstractValidator<SendVerificationEmailCommand>
{
    public SendVerificationEmailCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");
    }
}