namespace Identity.Application.Features.Authentication.SendVerificationEmail;

using MediatR;

public sealed record SendVerificationEmailCommand(
    string Email)
    : IRequest<SendVerificationEmailResponse>;