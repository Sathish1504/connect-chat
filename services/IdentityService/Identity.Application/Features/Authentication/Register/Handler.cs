using BCrypt.Net;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using MediatR;

namespace Identity.Application.Features.Authentication.Register;

public sealed class Handler : IRequestHandler<Command, Response>
{
    private readonly IUserRepository _repository;

    public Handler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> Handle(
        Command request,
        CancellationToken cancellationToken)
    {
        if (await _repository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new Exception("Email already exists.");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

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