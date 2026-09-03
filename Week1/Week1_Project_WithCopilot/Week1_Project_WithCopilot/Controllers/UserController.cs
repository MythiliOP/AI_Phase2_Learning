using Microsoft.AspNetCore.Mvc;
using Week1_Project_WithCopilot.Models;
using Week1_Project_WithCopilot.Services;

namespace Week1_Project_WithCopilot.Controllers;

[ApiController]
[Route("api/user")]
public sealed class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<User>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<User>> Get(CancellationToken cancellationToken)
    {
        var user = await userService.GetAsync(cancellationToken);

        return user is null ? NotFound() : Ok(user);
    }
}
