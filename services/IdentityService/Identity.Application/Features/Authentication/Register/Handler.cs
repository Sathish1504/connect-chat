using Identity.Application.Interfaces;
using Identity.Application.Interfaces.Security;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Authentication.Register;

public sealed class Handler : IRequestHandler<Command, Response>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public Handler(
        IUserRepository repository,
        IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        if (await _repository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new Exception("Email already exists.");
        }

        var passwordHash = _passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.UserName,
            request.Email,
            passwordHash);

        await _repository.AddAsync(user, cancellationToken);

        await _repository.SaveChangesAsync(cancellationToken);

        return new Response(
            user.Id,
            user.UserName,
            user.Email);
    }
}