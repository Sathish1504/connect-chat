using MediatR;

namespace Identity.Application.Features.Users.UploadProfilePicture;

public sealed record Command(
    Stream FileStream,
    string FileName,
    string ContentType,
    long FileSize
) : IRequest<Response>;