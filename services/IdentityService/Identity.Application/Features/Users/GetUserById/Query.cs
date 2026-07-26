using MediatR;

namespace Identity.Application.Features.Users.GetUserById;

public sealed record Query(Guid Id)
    : IRequest<Response>;