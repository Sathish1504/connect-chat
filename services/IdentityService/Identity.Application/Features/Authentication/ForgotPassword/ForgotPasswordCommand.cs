using MediatR;

namespace Identity.Application.Features.Authentication.ForgotPassword;

public sealed record ForgotPasswordCommand(
    string Email)
    : IRequest<ForgotPasswordResponse>;