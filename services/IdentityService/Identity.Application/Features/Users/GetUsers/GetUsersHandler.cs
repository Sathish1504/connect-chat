using Identity.Application.Interfaces;
using MediatR;

namespace Identity.Application.Features.Users.GetUsers;

public sealed class GetUsersHandler
    : IRequestHandler<GetUsersQuery, List<GetUsersResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetUsersHandler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<List<GetUsersResponse>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserService.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var users = await _userRepository.GetAllAsync(cancellationToken);

        return users
            .Where(x => x.Id != _currentUserService.UserId)
            .OrderBy(x => x.UserName)
            .Select(x => new GetUsersResponse(
                x.Id,
                x.UserName,
                x.Email,
                x.IsOnline))
            .ToList();
    }
}