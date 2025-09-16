using Microsoft.Extensions.Logging;

using DbRepos;
using Models.DTO;
using Models;

namespace Services;
    
public class ReviewServiceDb : IReviewService
{
    private readonly ReviewDbRepos _repo = null;
    private readonly ILogger<ReviewServiceDb> _logger = null;

    public Task<ResponseItemDto<IReview>> DeleteReviewAsync(Guid id) => _repo.DeleteReviewAsync(id);

    public Task<ResponsePageDto<IReview>> ReadReviewsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _repo.ReadReviewsAsync(seeded, flat, filter, pageNumber, pageSize);

    public Task<ResponseItemDto<IReview>> CreateReviewAsync(ReviewCuDto ReviewCuDto) => _repo.CreateReviewAsync(ReviewCuDto);

    #region constructors
    public ReviewServiceDb(ReviewDbRepos repo)
    {
        _repo = repo;
    }
    public ReviewServiceDb(ReviewDbRepos repo, ILogger<ReviewServiceDb> logger):this(repo)
    {
        _logger = logger;
    }


    #endregion
}