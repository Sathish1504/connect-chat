using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.GetProfile;

public sealed class Handler : IRequestHandler<Query, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public Handler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Response> Handle(
        Query request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var user = await _userRepository.GetByIdAsync(
            _currentUserService.UserId,
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
            user.EmailConfirmed,
            user.IsOnline,
            user.IsActive,
            user.CreatedAt,
            user.LastSeenAt);
    }
}