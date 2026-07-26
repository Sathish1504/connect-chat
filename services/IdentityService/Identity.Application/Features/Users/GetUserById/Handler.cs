using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.GetUserById;

public sealed class Handler
    : IRequestHandler<Query, Response>
{
    private readonly IUserRepository _userRepository;

    public Handler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(
        Query request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        return new Response(
            user.Id,
            user.UserName,
            user.Email,
            user.ProfilePicture,
            user.IsOnline);
    }
}