using MediatR;

namespace Identity.Application.Features.Users.GetProfile;

public sealed record Query : IRequest<Response>;