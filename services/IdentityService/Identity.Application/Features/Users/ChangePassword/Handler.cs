using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.ChangePassword;

public sealed class Handler : IRequestHandler<Command, Response>
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
        Command request,
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

        var isValidPassword = BCrypt.Net.BCrypt.Verify(
            request.CurrentPassword,
            user.PasswordHash);

        if (!isValidPassword)
        {
            throw new UnauthorizedAccessException("Current password is incorrect.");
        }

        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(
            request.NewPassword);

        user.ChangePassword(newPasswordHash);

        // Security: revoke existing refresh token
        user.RevokeRefreshToken();

        await _userRepository.SaveChangesAsync(cancellationToken);

        return new Response("Password changed successfully.");
    }
}