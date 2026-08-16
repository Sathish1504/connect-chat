using Identity.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Identity.Infrastructure.Services;

public sealed class LocalFileStorage : IFileStorage
{
    private const string UploadDirectory =
        "uploads/profile-pictures";

    private readonly IWebHostEnvironment _environment;

    public LocalFileStorage(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveAsync(
        Stream fileStream,
        string fileExtension,
        CancellationToken cancellationToken)
    {
        var uploadPath = Path.Combine(
            _environment.ContentRootPath,
            UploadDirectory);

        Directory.CreateDirectory(uploadPath);

        var fileName =
            $"{Guid.NewGuid():N}{fileExtension.ToLowerInvariant()}";

        var filePath = Path.Combine(
            uploadPath,
            fileName);

        await using var outputStream =
            new FileStream(
                filePath,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);

        await fileStream.CopyToAsync(
            outputStream,
            cancellationToken);

        return $"/{UploadDirectory.Replace('\\', '/')}/{fileName}";
    }

    public Task DeleteAsync(
        string? relativePath,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            return Task.CompletedTask;
        }

        var normalizedPath =
            relativePath.TrimStart('/')
                         .Replace('/', Path.DirectorySeparatorChar);

        var rootPath = Path.GetFullPath(
            _environment.ContentRootPath);

        var filePath = Path.GetFullPath(
            Path.Combine(
                _environment.ContentRootPath,
                normalizedPath));

        if (!filePath.StartsWith(
                rootPath,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "Invalid file path.");
        }

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return Task.CompletedTask;
    }
}