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

    public SightController(ILogger<SightController> logger, ISightService service)
    {
        _logger = logger;
        _service = service;

    }
    [HttpGet()]
    [ActionName("Read")]
    [ProducesResponseType(200, Type = typeof(ResponsePageDto<ISight>))]
    public async Task<IActionResult> AllSights(string seeded = "true", string flat = "true", string filter = null, string pageNumber = "0", string pageSize = "10")
    {
        try
        {
            bool seededArg = bool.Parse(seeded);
            bool flatArg = bool.Parse(flat);
            int pageNrArg = int.Parse(pageNumber);
            int pageSizeArg = int.Parse(pageSize);
            _logger.LogInformation($"{nameof(AllSights)}");
            var sights = await _service.ReadSightsAsync(seededArg, flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);

            return Ok(sights);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(AllSights)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{Id}")]
    [ActionName("ReadItem")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Sight(string Id, string flat = "false")
    {
        try
        {
            var idArg = Guid.Parse(Id);
            var flatArg = bool.Parse(flat);

            var sight = await _service.ReadSightAsync(idArg, flatArg);
            return Ok(sight);
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
    public async Task<IActionResult> NoReview(string seeded = "true", string flat = "true", string pageNumber = "0", string pageSize = "10")
    {
        try
        {
            bool seededArg = bool.Parse(seeded);
            bool flatArg = bool.Parse(flat);
            int pageNrArg = int.Parse(pageNumber);
            int pageSizeArg = int.Parse(pageSize);
            var sights = await _service.ReadSightsNoReviewAsync(seededArg, flatArg, pageNrArg, pageSizeArg);
            return Ok(sights);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }






    [HttpPut("{Id}")]
    [ActionName("UpdateItem")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateSight(string Id)
    {
        try
        {
            var idArg = Guid.Parse(Id);

            var sight = await _service.DeleteSightAsync(idArg);
            return Ok(sight);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost()]
    [ActionName("CreateItem")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> CreateSight(string Id)
    {
        try
        {
            var idArg = Guid.Parse(Id);

            var sight = await _service.DeleteSightAsync(idArg);
            return Ok(sight);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{Id}")]
    [ActionName("DeleteItem")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveSight(string Id)
    {
        try
        {
            var idArg = Guid.Parse(Id);

            var sight = await _service.DeleteSightAsync(idArg);
            return Ok(sight);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

}