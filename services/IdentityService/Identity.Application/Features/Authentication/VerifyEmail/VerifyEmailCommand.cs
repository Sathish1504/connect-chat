using MediatR;

namespace Identity.Application.Features.Authentication.VerifyEmail;

public sealed record VerifyEmailCommand(
    string Token)
    : IRequest<VerifyEmailResponse>;