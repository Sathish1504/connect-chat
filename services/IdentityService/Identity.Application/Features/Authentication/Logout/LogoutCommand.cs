using MediatR;

namespace Identity.Application.Features.Authentication.Logout;

public sealed record LogoutCommand()
    : IRequest<LogoutResponse>;