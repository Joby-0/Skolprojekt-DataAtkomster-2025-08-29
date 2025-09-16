namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;
using Services;
using System.Threading.Tasks;
using Models.DTO;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    readonly IUserService _service;
    public UserController(ILogger<UserController> logger, IUserService service)
    {
        _logger = logger;
        _service = service;

    }
    [HttpGet()]
    [ActionName("Read")]
    [ProducesResponseType(200)]

     public async Task<IActionResult> AllUsers(string seeded = "true", string flat = "true", string filter = null, string pageNumber = "0", string pageSize = "10")
    {
        try
        {
            bool seededArg = bool.Parse(seeded);
            bool flatArg = bool.Parse(flat);
            int pageNrArg = int.Parse(pageNumber);
            int pageSizeArg = int.Parse(pageSize);
            _logger.LogInformation($"{nameof(AllUsers)}");
            var users = await _service.ReadUsersAsync(seededArg, flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);

            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(AllUsers)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ActionName("ReadItem")]
    [ProducesResponseType(200)]

    public async Task<IActionResult> ReadItem(string id, string flat = "false")
    {
        try
        {
            bool flatArg = bool.Parse(flat);
            Guid idArg = Guid.Parse(id);

            var user = await _service.ReadUserAsync(idArg, flatArg);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost()]
    [ActionName("AddItem")]
    [ProducesResponseType(200)]

    public async Task<IActionResult> AddUser([FromBody] UserCuDto userCuDto)
    {
        try
        {
            var user = await _service.CreateUserAsync(userCuDto);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{Id}")]
    [ActionName("RemoveItem")]
    [ProducesResponseType(200)]

    public async Task<IActionResult> RemoveUser(string Id)
    {
        try
        {
            var idArg = Guid.Parse(Id);
            var item = await _service.RemoveUserAsync(idArg);
            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

}
