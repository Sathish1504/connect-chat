using Identity.Application.Features.Users.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChangePasswordCommand = Identity.Application.Features.Users.ChangePassword.Command;
using ChangePasswordResponse = Identity.Application.Features.Users.ChangePassword.Response;
using UpdateProfileCommand = Identity.Application.Features.Users.UpdateProfile.Command;
using UpdateProfileResponse = Identity.Application.Features.Users.UpdateProfile.Response;
using Identity.Application.Features.Users.GetUsers;
using GetUserByIdQuery = Identity.Application.Features.Users.GetUserById.Query;
using GetUserByIdResponse = Identity.Application.Features.Users.GetUserById.Response;
using UploadProfilePictureCommand =Identity.Application.Features.Users.UploadProfilePicture.Command;
using UploadProfilePictureResponse =Identity.Application.Features.Users.UploadProfilePicture.Response;
using DeleteProfilePictureCommand = Identity.Application.Features.Users.DeleteProfilePicture.Command;
using DeleteProfilePictureResponse = Identity.Application.Features.Users.DeleteProfilePicture.Response;

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

    [HttpPut("profile")]
    [ProducesResponseType(typeof(UpdateProfileResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UpdateProfileResponse>> UpdateProfile(
    [FromBody] UpdateProfileCommand command,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

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

    [HttpGet]
    [ProducesResponseType(typeof(List<GetUsersResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetUsersResponse>>> GetUsers(
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetUsersQuery(),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetUserByIdResponse>> GetUserById(
    Guid id,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetUserByIdQuery(id),
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("profile-picture")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(
     typeof(UploadProfilePictureResponse),
     StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UploadProfilePictureResponse>> UploadProfilePicture(
     IFormFile file,
     CancellationToken cancellationToken)
    {
        if (file is null)
        {
            return BadRequest("Profile picture file is required.");
        }

        await using var stream = file.OpenReadStream();

        var command = new UploadProfilePictureCommand(
            stream,
            file.FileName,
            file.ContentType,
            file.Length);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(result);
    }

    [HttpDelete("profile-picture")]
    [ProducesResponseType(
    typeof(DeleteProfilePictureResponse),
    StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteProfilePictureResponse>> DeleteProfilePicture(
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new DeleteProfilePictureCommand(),
            cancellationToken);

        return Ok(result);
    }
}