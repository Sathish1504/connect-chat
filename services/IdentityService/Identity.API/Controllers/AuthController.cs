using Identity.Application.Features.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Response>> Register(Command command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}