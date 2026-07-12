using MediatR;

namespace Identity.Application.Features.Authentication.Register;

public sealed record RegisterCommand(
    string UserName,
    string Email,
    string Password
) : IRequest<RegisterResponse>;