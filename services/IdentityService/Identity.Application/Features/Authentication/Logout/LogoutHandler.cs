using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Authentication.Logout;

public sealed class LogoutHandler
    : IRequestHandler<LogoutCommand, LogoutResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public LogoutHandler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<LogoutResponse> Handle(
        LogoutCommand request,
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
            throw new UnauthorizedAccessException("User not found.");
        }

        user.RevokeRefreshToken();

        await _userRepository.SaveChangesAsync(cancellationToken);

        return new LogoutResponse("Logged out successfully.");
    }
}