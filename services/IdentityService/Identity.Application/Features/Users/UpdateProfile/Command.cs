using MediatR;

namespace Identity.Application.Features.Users.UpdateProfile;

public sealed record Command(
    string UserName,
    string? ProfilePicture
) : IRequest<Response>;