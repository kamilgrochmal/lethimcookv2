using LetHimCookV2.API.Application.DTO;
using LetHimCookV2.API.Application.Requests;
using LetHimCookV2.API.Application.Security;
using LetHimCookV2.API.Domain.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LetHimCookV2.API.API;

[ApiController]
[Route("[controller]")]
public class UsersController : BaseController
{
    private readonly IIdentityService _identityService;

    public UsersController(
        IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation("Returns basic user info")]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> Get()
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }
        var userId = Int64.Parse(User.Identity.Name);
        var user = await _identityService.GetAsync(userId);

        return user;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Creates new patient account")]
    public async Task<ActionResult> Post(SignUp signUp)
    {
        await _identityService.SignUpAsync(signUp);
        return Created();
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation("Authenticates user")]
    public async Task<ActionResult<JsonWebToken>> Post(SignIn signIn)
    {
        return Ok(await _identityService.SignInAsync(signIn));
    }
}