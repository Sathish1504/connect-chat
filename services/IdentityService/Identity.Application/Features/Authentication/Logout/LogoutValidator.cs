using FluentValidation;

namespace Identity.Application.Features.Authentication.Logout;

public sealed class LogoutValidator
    : AbstractValidator<LogoutCommand>
{
}