namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;
using Services;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;

    }
    [HttpGet()]
    [ActionName("AllUsers")]
    [ProducesResponseType(200)]

    public IActionResult AllUsers()
    {
        try
        {
            return Ok(new SeedGenerator().ShortComment);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
    [HttpPost()]
    [ActionName("AddUser")]
    [ProducesResponseType(200)]

    public IActionResult AddUser()
    {
        try
        {
            return Ok(new SeedGenerator().ShortComment);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{UserId}")]
    [ActionName("RemoveUser")]
    [ProducesResponseType(200)]

    public IActionResult RemoveUser(string UserId)
    {
        try
        {
            return Ok(new SeedGenerator().ShortComment);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
}
