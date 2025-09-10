namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;
using Services;
using Models.DTO;

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
    [ProducesResponseType(200, Type = typeof(ResponsePageDto<ISight>))]
    public async Task<IActionResult> AllSights(string seeded = "true", string flat = "true", string filter = null, string pageNumber = "0", string pageSize = "10")
    {
        try
        {
            bool seededArg = bool.Parse(seeded);
            bool flatArg = bool.Parse(flat);
            int pageNrArg = int.Parse(pageNumber);
            int pageSizeArg = int.Parse(pageSize);
            _logger.LogInformation($"{nameof(SeedSights)}");
            var sights = await _service.ReadSightsAsync(seededArg, flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);

            return Ok(sights);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(SeedSights)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet()]
    [ActionName("SeedSights")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> SeedSights(string nrOfItems = "1000")
    {
        try
        {
            _logger.LogInformation($"{nameof(SeedSights)}");
            await _service.SeedAsync(nrOfItems);

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