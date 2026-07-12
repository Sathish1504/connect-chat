using MediatR;

namespace Identity.Application.Features.Authentication.Register;

public sealed record Command(
    string UserName,
    string Email,
    string Password
) : IRequest<Response>;