using Identity.Application.Features.Users.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChangePasswordCommand = Identity.Application.Features.Users.ChangePassword.Command;
using ChangePasswordResponse = Identity.Application.Features.Users.ChangePassword.Response;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response>> GetProfile(
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new Query(),
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("change-password")]
    [ProducesResponseType(typeof(ChangePasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ChangePasswordResponse>> ChangePassword(
    ChangePasswordCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}