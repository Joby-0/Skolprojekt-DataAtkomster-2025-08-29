namespace AppWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Joby.Utilities.SeedGenerator;
using Models;
using Services;
using System.Threading.Tasks;
using Models.DTO;

[ApiController]
[Route("api/[controller]/[action]")]
public class ReviewController : Controller
{
    private readonly ILogger<ReviewController> _logger;
    readonly IReviewService _service;
    public ReviewController(IReviewService service, ILogger<ReviewController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet()]
    [ActionName("Read")]
    [ProducesResponseType(200)]

    public async Task<IActionResult> AllReviews(string seeded = "true", string flat = "true", string filter = null, string pageNumber = "0", string pageSize = "10")
    {


        try
        {
            bool seededArg = bool.Parse(seeded);
            bool flatArg = bool.Parse(flat);
            int pageNrArg = int.Parse(pageNumber);
            int pageSizeArg = int.Parse(pageSize);
            _logger.LogInformation($"{nameof(AllReviews)}");
            var reviews = await _service.ReadReviewsAsync(seededArg, flatArg, filter?.Trim().ToLower(), pageNrArg, pageSizeArg);

            return Ok(reviews);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(AllReviews)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{Id}")]
    [ActionName("Delete")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveReview(string Id)
    {
        try
        {
            var idArg = Guid.Parse(Id);
            var item = await _service.DeleteReviewAsync(idArg);
            return Ok(item);
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
    public async Task<IActionResult> CreateReview([FromBody] ReviewCuDto reviewCuDto)
    {
        try
        {
            var item = await _service.CreateReviewAsync(reviewCuDto);
            return Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(Environment)}: {ex.Message}");
            return BadRequest(ex.Message);
        }
    }
}