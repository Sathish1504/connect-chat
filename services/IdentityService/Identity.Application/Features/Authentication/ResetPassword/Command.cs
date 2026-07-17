using MediatR;

namespace Identity.Application.Features.Authentication.ResetPassword;

public sealed record Command(
    string Token,
    string NewPassword,
    string ConfirmPassword)
    : IRequest<Response>;