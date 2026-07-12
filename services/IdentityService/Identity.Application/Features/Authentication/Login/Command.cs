using MediatR;

namespace Identity.Application.Features.Authentication.Login;

public sealed record Command(
    string Email,
    string Password)
    : IRequest<Response>;