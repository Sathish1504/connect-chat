using MediatR;

namespace Identity.Application.Features.Users.DeleteProfilePicture;

public sealed record Command : IRequest<Response>;