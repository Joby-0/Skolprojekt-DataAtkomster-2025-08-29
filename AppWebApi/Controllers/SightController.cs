namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;
using Services;

[ApiController]
[Route("api/[controller]/[action]")]
public class SightController : Controller
{
    private readonly ILogger<SightController> _logger;
    readonly ISightService _service;

    private readonly SeedGenerator _seeder = new SeedGenerator();
    // private readonly ISightService sightService;

    public SightController(ILogger<SightController> logger, ISightService service)
    {
        _logger = logger;
        _service = service;

    }

    [HttpGet()]
    [ActionName("AllSights")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> SeedSights()
    {
        try
        {
            _logger.LogInformation($"{nameof(SeedSights)}");
            await _service.SeedAsync();

            return Ok("Seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(SeedSights)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet()]
    [ActionName("Sightsnoreview")]
    [ProducesResponseType(200)]
    public IActionResult NoReview()
    {
        try
        {

            return Ok(new SeedGenerator().RandomCategory());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


    [HttpGet()]
    [ActionName("Sight")]
    [ProducesResponseType(200)]
    public IActionResult Sight(string SightId)
    {
        try
        {
            return Ok(new SeedGenerator().RandomCategory());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{SightId}")]
    [ActionName("RemoveSight")]
    [ProducesResponseType(200)]
    public IActionResult RemoveSight(string SightId)
    {
        try
        {
            return Ok(new SeedGenerator().Sight());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

}