namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;

[ApiController]
[Route("api/[controller]/[action]")]
public class SightController : Controller
{
    private readonly ILogger<SightController> _logger;
    private readonly SeedGenerator _seeder = new SeedGenerator();

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
            var Address = _seeder.ItemsToList<Address>(10);
            var City = _seeder.ItemsToList<City>(10);
            var Country = _seeder.ItemsToList<Country>(10);
            for (int i = 0; i < Country.Count; i++)
            {
                City[i].Country = Country[i];
            }
            for (int i = 0; i < Address.Count; i++)
            {
                Address[i].City = City[i];
            }
            for (int i = 0; i < Sights.Count; i++)
            {
                Sights[i].Address = Address[i];

            }
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