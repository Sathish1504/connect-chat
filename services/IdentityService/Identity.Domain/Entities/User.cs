public class User
{
    public Guid Id { get; private set; }

    public string UserName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string? ProfilePicture { get; private set; }

    public bool IsOnline { get; private set; }

    public bool EmailConfirmed { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public DateTime? LastSeenAt { get; private set; }

    private User() { }

    public User(string userName, string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;

        IsActive = true;
        EmailConfirmed = false;
        IsOnline = false;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetOnline(bool online)
    {
        IsOnline = online;

        if (!online)
            LastSeenAt = DateTime.UtcNow;

        UpdatedAt = DateTime.UtcNow;
    }

    public void ConfirmEmail()
    {
        EmailConfirmed = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangePassword(string hash)
    {
        PasswordHash = hash;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfilePicture(string picture)
    {
        ProfilePicture = picture;
        UpdatedAt = DateTime.UtcNow;
    }
}