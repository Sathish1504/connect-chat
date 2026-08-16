namespace Identity.Application.Interfaces;

public interface IFileStorage
{
    Task<string> SaveAsync(
        Stream fileStream,
        string fileExtension,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        string? relativePath,
        CancellationToken cancellationToken);
}