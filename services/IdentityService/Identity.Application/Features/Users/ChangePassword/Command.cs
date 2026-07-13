using MediatR;

namespace Identity.Application.Features.Users.ChangePassword;

public sealed record Command(
    string CurrentPassword,
    string NewPassword,
    string ConfirmPassword
) : IRequest<Response>;