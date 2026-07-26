using Identity.Domain.Entities;

namespace Identity.Application.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task<User?> GetByEmailVerificationTokenAsync(
        string token,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        User user,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);

    Task<User?> GetByRefreshTokenAsync(
    string refreshToken,
    CancellationToken cancellationToken);

    Task UpdateAsync(
        User user,
        CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken);

    Task<User?> GetByPasswordResetTokenAsync(
    string token,
    CancellationToken cancellationToken = default);

    Task<List<User>> GetAllAsync(
    CancellationToken cancellationToken);
}