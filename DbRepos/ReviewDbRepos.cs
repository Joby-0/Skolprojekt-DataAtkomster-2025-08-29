namespace DbRepos;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using DbModels;
using DbContext;
using Configuration;
using Joby.Utilities.SeedGenerator;
using Models;
using Models.DTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ReviewDbRepos
{
    private readonly ILogger<ReviewDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;
    public async Task<ResponsePageDto<IReview>> ReadReviewsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<ReviewDbM> query;
        if (flat)
        {
            query = _dbContext.Reviews.AsNoTracking();
        }
        else
        {
            query = _dbContext.Reviews
            .Include(r => r.SightDbM)
            .Include(r => r.UserDbM)
            .AsNoTracking();

        }

        var ret = new ResponsePageDto<IReview>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            DbItemsCount = await query.CountAsync(),

            // .Where(i => (i.Seeded == seeded)),

            PageItems = await query

            // .Where(i => (i.Seeded == seeded))


            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync<IReview>(),

            PageNr = pageNumber,
            PageSize = pageSize


        };
        return ret;
    }
    public async Task<ResponseItemDto<IReview>> DeleteReviewAsync(Guid id)
    {
        var query = _dbContext.Reviews
            .Where(i => i.ReviewId == id);
        var item = await query.FirstOrDefaultAsync<ReviewDbM>();


        _dbContext.Reviews.Remove(item);
        await _dbContext.SaveChangesAsync();


        return new ResponseItemDto<IReview>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }

    public async Task<ResponseItemDto<IReview>> CreateReviewAsync(ReviewCuDto reviewCuDto)
    {
         if (reviewCuDto.ReviewId != null)
            throw new ArgumentException($"{nameof(reviewCuDto.ReviewId)} must be null when creating a new object");

        var item = new ReviewDbM(reviewCuDto);

        await navProp_ReviewCUdto_to_ReviewDbM(reviewCuDto, item);

        _dbContext.Reviews.Add(item);

        await _dbContext.SaveChangesAsync();

        return null;
    }
    public ReviewDbRepos(ILogger<ReviewDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }

    private async Task navProp_ReviewCUdto_to_ReviewDbM(ReviewCuDto itemDtoSrc, ReviewDbM itemDst)
    {
        
        itemDst.SightDbM = await _dbContext.Sights.FirstOrDefaultAsync(a => a.SightId == itemDtoSrc.SightId);

        itemDst.UserDbM = await _dbContext.Users.FirstOrDefaultAsync(a => a.UserId == itemDtoSrc.UserId);

    }
}