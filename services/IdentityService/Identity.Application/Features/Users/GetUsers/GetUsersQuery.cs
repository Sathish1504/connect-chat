using MediatR;

namespace Identity.Application.Features.Users.GetUsers;

public sealed record GetUsersQuery : IRequest<List<GetUsersResponse>>;