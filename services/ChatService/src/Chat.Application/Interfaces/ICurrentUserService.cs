namespace Chat.Application.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
}