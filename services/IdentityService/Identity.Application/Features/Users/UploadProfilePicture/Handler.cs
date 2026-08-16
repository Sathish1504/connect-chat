using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.UploadProfilePicture;

public sealed class Handler : IRequestHandler<Command, Response>
{
    private const long MaxFileSize = 5 * 1024 * 1024;

    private static readonly Dictionary<string, string> AllowedFileTypes =
        new(StringComparer.OrdinalIgnoreCase)
        {
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".png"] = "image/png",
            [".webp"] = "image/webp"
        };

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

        if (request.FileStream is null ||
            !request.FileStream.CanRead)
        {
            throw new ArgumentException(
                "A valid image file is required.");
        }

        if (request.FileSize <= 0)
        {
            throw new ArgumentException(
                "The uploaded file is empty.");
        }

        if (request.FileSize > MaxFileSize)
        {
            throw new ArgumentException(
                "Profile picture must not exceed 5 MB.");
        }

        var extension = Path.GetExtension(request.FileName);

        if (string.IsNullOrWhiteSpace(extension) ||
            !AllowedFileTypes.TryGetValue(
                extension,
                out var expectedContentType))
        {
            throw new ArgumentException(
                "Only JPG, JPEG, PNG and WEBP images are allowed.");
        }

        if (!string.Equals(
                request.ContentType,
                expectedContentType,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException(
                "The uploaded file type is invalid.");
        }

        var user = await _userRepository.GetByIdAsync(
            _currentUserService.UserId,
            cancellationToken);

        if (user is null)
        {
            throw new KeyNotFoundException(
                "User not found.");
        }

        var oldProfilePicture = user.ProfilePicture;

        var profilePictureUrl = await _fileStorage.SaveAsync(
            request.FileStream,
            extension,
            cancellationToken);

        try
        {
            user.UpdateProfilePicture(profilePictureUrl);

            await _userRepository.SaveChangesAsync(
                cancellationToken);

            if (!string.IsNullOrWhiteSpace(oldProfilePicture))
            {
                await _fileStorage.DeleteAsync(
                    oldProfilePicture,
                    cancellationToken);
            }

            return new Response(profilePictureUrl);
        }
        catch
        {
            await _fileStorage.DeleteAsync(
                profilePictureUrl,
                cancellationToken);

            throw;
        }
    }
}