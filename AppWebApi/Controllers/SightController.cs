namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;
using Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class SightController : Controller
{
    private readonly ILogger<SightController> _logger;
    private readonly SeedGenerator _seeder = new SeedGenerator();
    private readonly ISightService sightService;

    public SightController(ILogger<SightController> logger)
    {
        _logger = logger;

    }

    [HttpGet()]
    [ActionName("AllSights")]
    [ProducesResponseType(200)]
    public IActionResult AllSights()
    {
        try
        {
            var Sights = _seeder.ItemsToList<Sight>(10);
            return Ok(Sights);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
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
    public IActionResult Sight()
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
}