using FluentValidation;

namespace Identity.Application.Features.Users.UpdateProfile;

public sealed class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.ProfilePicture)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.ProfilePicture));
    }
}