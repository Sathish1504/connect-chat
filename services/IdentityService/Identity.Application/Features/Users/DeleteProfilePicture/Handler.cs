using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.DeleteProfilePicture;

public sealed class Handler : IRequestHandler<Command, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IFileStorage _fileStorage;

    public Handler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService,
        IFileStorage fileStorage)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _fileStorage = fileStorage;
    }

    public async Task<Response> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            throw new UnauthorizedAccessException(
                "User is not authenticated.");
        }

        var user = await _userRepository.GetByIdAsync(
            _currentUserService.UserId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException(
                "User not found.");
        }

        if (string.IsNullOrWhiteSpace(user.ProfilePicture))
        {
            return new Response(
                "Profile picture is already removed.");
        }

        var profilePicture = user.ProfilePicture;

        await _fileStorage.DeleteAsync(
            profilePicture,
            cancellationToken);

        user.RemoveProfilePicture();

        await _userRepository.SaveChangesAsync(
            cancellationToken);

        return new Response(
            "Profile picture removed successfully.");
    }
}