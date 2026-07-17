using LoginCommand = Identity.Application.Features.Authentication.Login.Command;
using LoginResponse = Identity.Application.Features.Authentication.Login.Response;
using RegisterCommand = Identity.Application.Features.Authentication.Register.Command;
using RegisterResponse = Identity.Application.Features.Authentication.Register.Response;
using RefreshTokenCommand = Identity.Application.Features.Authentication.Refresh.RefreshTokenCommand;
using RefreshTokenResponse = Identity.Application.Features.Authentication.Refresh.RefreshTokenResponse;
using LogoutCommand = Identity.Application.Features.Authentication.Logout.LogoutCommand;
using LogoutResponse = Identity.Application.Features.Authentication.Logout.LogoutResponse;
using SendVerificationEmailCommand =
    Identity.Application.Features.Authentication.SendVerificationEmail.SendVerificationEmailCommand;
using SendVerificationEmailResponse =
    Identity.Application.Features.Authentication.SendVerificationEmail.SendVerificationEmailResponse;
using Identity.Application.Features.Authentication.VerifyEmail;
using Identity.Application.Features.Authentication.ForgotPassword;
using ResetPasswordCommand = Identity.Application.Features.Authentication.ResetPassword.Command;
using ResetPasswordResponse = Identity.Application.Features.Authentication.ResetPassword.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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
    [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<RegisterResponse>> Register(
        RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login(
        LoginCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub),

            UserName = User.Identity?.Name,

            Email = User.FindFirstValue(ClaimTypes.Email)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Email)
        });
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(RefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RefreshTokenResponse>> Refresh(
    RefreshTokenCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(typeof(LogoutResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LogoutResponse>> Logout(
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new LogoutCommand(),
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("send-verification-email")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SendVerificationEmailResponse>> SendVerificationEmail(
    [FromBody] SendVerificationEmailCommand command,
    CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpGet("verify-email")]
    [ProducesResponseType(typeof(VerifyEmailResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<VerifyEmailResponse>> VerifyEmail(
    [FromQuery] string token,
    CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new VerifyEmailCommand(token),
            cancellationToken);

        return Ok(response);
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ForgotPasswordResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ForgotPasswordResponse>> ForgotPassword(
    [FromBody] ForgotPasswordCommand command,
    CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ResetPasswordResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResetPasswordResponse>> ResetPassword(
    [FromBody] ResetPasswordCommand command,
    CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }
}