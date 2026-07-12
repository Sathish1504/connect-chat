using Identity.Application.Common.Authentication;
using Identity.Domain.Entities;

namespace Identity.Application.Interfaces;

public interface IJwtTokenGenerator
{
    AccessTokenResult GenerateAccessToken(User user);

    string GenerateRefreshToken();
}