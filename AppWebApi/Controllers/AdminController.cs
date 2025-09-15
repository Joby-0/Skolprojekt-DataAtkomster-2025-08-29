namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;
using Services;
using Models.DTO;

[ApiController]
[Route("api/[controller]/[action]")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    readonly IAdminService _service;

    private readonly SeedGenerator _seeder = new SeedGenerator();

    public AdminController(ILogger<AdminController> logger, IAdminService service)
    {
        _logger = logger;
        _service = service;

    }

    [HttpGet()]
    [ActionName("Seed")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Seed(string nrOfItems = "1000")
    {
        try
        {
            _logger.LogInformation($"{nameof(Seed)}");
            await _service.SeedAsync(nrOfItems);

            return Ok("Seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Seed)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet()]
    [ActionName("DbInfo")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DbInfo()
    {
        try
        {
            // _logger.LogInformation($"{nameof(Seed)}");

            return Ok(await _service.DbInfo());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Seed)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete()]
    [ActionName("RemoceSeeded")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveSeed()
    {
        try
        {
            _logger.LogInformation($"{nameof(Seed)}");
            await _service.RemoveSeedAsync();

            return Ok("Remove seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Seed)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
}