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

public class SightDbRepos
{
    private readonly ILogger<SightDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;


    public async Task<ResponsePageDto<ISight>> ReadSightsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<SightDbM> query;
        if (flat)
        {
            query = _dbContext.Sights.AsNoTracking();
        }
        else
        {
            query = _dbContext.Sights
            .Include(s => s.AddressDbM)
                .ThenInclude(a => a.CityDbM)
                    .ThenInclude(c => c.CountryDbM)
            .Include(s => s.CategoryDbMs)
            .Include(s => s.ReviewDbMs)
                .ThenInclude(r => r.UserDbM)
            .AsNoTracking();
        }

        var ret = new ResponsePageDto<ISight>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            DbItemsCount = await query

            .Where(i => (i.Seeded == seeded)
             && i.AddressDbM.Street.ToLower().Contains(filter)
             || i.AddressDbM.CityDbM.CityName.ToLower().Contains(filter)
             || i.AddressDbM.CityDbM.CountryDbM.CountryName.ToLower().Contains(filter)).CountAsync(),

            PageItems = await query

            .Where(i => (i.Seeded == seeded)
             && i.AddressDbM.Street.ToLower().Contains(filter)
             || i.AddressDbM.CityDbM.CityName.ToLower().Contains(filter)
             || i.AddressDbM.CityDbM.CountryDbM.CountryName.ToLower().Contains(filter))

            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .OrderBy(s => s.CategoryDbMs.FirstOrDefault())
                .ThenBy(s => s.SightName)
                    .ThenBy(s => s.Description)
                        .ThenBy(s => s.AddressDbM.CityDbM.CountryDbM)
                            .ThenBy(s => s.AddressDbM.CityDbM)
                                .ThenByDescending(s => s.CategoryDbMs.FirstOrDefault())
            .ToListAsync<ISight>(),

            PageNr = pageNumber,
            PageSize = pageSize


        };
        return ret;
    }

    public async Task<ResponseItemDto<ISight>> ReadSightAsync(Guid id, bool flat)
    {
        IQueryable<SightDbM> query;
        if (flat)
        {
            query = _dbContext.Sights.AsNoTracking();
        }
        else
        {
            query = _dbContext.Sights
            .Include(s => s.AddressDbM)
                .ThenInclude(a => a.CityDbM)
                    .ThenInclude(c => c.CountryDbM)
            .Include(s => s.CategoryDbMs)
            .Include(s => s.ReviewDbMs)
                .ThenInclude(r => r.UserDbM)
            .AsNoTracking();
        }
        var ret = new ResponseItemDto<ISight>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = await query.FirstOrDefaultAsync(i => i.SightId == id)


        };
        return ret;

    }

    public async Task<ResponseItemDto<ISight>> DeleteSightAsync(Guid id)
    {
        var query = _dbContext.Sights
            .Where(i => i.SightId == id);
        var item = await query.FirstOrDefaultAsync<SightDbM>();

        _dbContext.RemoveRange(_dbContext.Reviews.Where(r => r.SightId == id));


        _dbContext.Sights.Remove(item);
        await _dbContext.SaveChangesAsync();
        return new ResponseItemDto<ISight>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }

    public async Task<ResponseItemDto<ISight>> UpdateSightAsync(SightCuDto sightCuDto)
    {
        var query1 = _dbContext.Sights.Where(s => s.SightId == sightCuDto.SightId);
        var item = await query1
        .Include(i => i.AddressDbM)
        .Include(i => i.CategoryDbMs)
        .Include(i => i.ReviewDbMs)
        .FirstOrDefaultAsync<SightDbM>();

        item.UpdateFromDTO(sightCuDto);
        
        await navProp_FriendCUdto_to_FriendDbM(sightCuDto, item);

        _dbContext.Sights.Update(item);

        await _dbContext.SaveChangesAsync();

        return await ReadSightAsync(item.SightId, false);
    }

    public async Task<ResponsePageDto<ISight>> ReadSightsNoReviewAsync(bool seeded, bool flat, int pageNumber, int pageSize)
    {
        IQueryable<SightDbM> query;
        if (flat)
        {
            query = _dbContext.Sights.AsNoTracking();
        }
        else
        {
            query = _dbContext.Sights
            .Include(s => s.AddressDbM)
                .ThenInclude(a => a.CityDbM)
                    .ThenInclude(c => c.CountryDbM)
            .Include(s => s.CategoryDbMs)
            .Include(s => s.ReviewDbMs)
                .ThenInclude(r => r.UserDbM)
            .AsNoTracking();
        }
        var ret = new ResponsePageDto<ISight>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            DbItemsCount = await query
            .Where(i => i.Seeded == seeded && !i.ReviewDbMs.Any())
            .CountAsync(),

            PageItems = await query
            .Where(i => i.Seeded == seeded && !i.ReviewDbMs.Any())


           .Skip(pageNumber * pageSize)
           .Take(pageSize)
           .ToListAsync<ISight>(),

            PageNr = pageNumber,
            PageSize = pageSize


        };
        return ret;
    }

    public async Task<ResponseItemDto<ISight>> CreateSightAsync(SightCuDto sightCuDto)
    {
        var item = new SightDbM(sightCuDto);

        await navProp_FriendCUdto_to_FriendDbM(sightCuDto, item);

        _dbContext.Sights.Add(item);

        await _dbContext.SaveChangesAsync();

        return await ReadSightAsync(item.SightId, false);
    }
    public SightDbRepos(ILogger<SightDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }

    private async Task navProp_FriendCUdto_to_FriendDbM(SightCuDto itemDtoSrc, SightDbM itemDst)
    {
        

        //update AddressDbM from itemDto.AddressId
        
        itemDst.AddressDbM = (itemDtoSrc.AddressId != null) ? await _dbContext.Addresses.FirstOrDefaultAsync(
            a => (a.AddressId == itemDtoSrc.AddressId)) : null;
        //update ReviewsDbM from itemDto.ReviewsId list
        List<ReviewDbM> reviews = null;
        if (itemDtoSrc.ReviewsId != null)
        {
            reviews = new List<ReviewDbM>();
            foreach (var id in itemDtoSrc.ReviewsId)
            {
                var p = await _dbContext.Reviews.FirstOrDefaultAsync(i => i.ReviewId == id);
                if (p == null)
                    throw new ArgumentException($"Item id {id} not existing");

                reviews.Add(p);
            }
        }
        itemDst.ReviewDbMs = reviews;

        //update Categories from itemDto.CategoryId
        List<CategoryDbM> categories = null;
        if (itemDtoSrc.CategoriesId != null)
        {
            categories = new List<CategoryDbM>();
            foreach (var id in itemDtoSrc.CategoriesId)
            {
                var q = await _dbContext.Categories.FirstOrDefaultAsync(i => i.CategoryId == id);
                if (q == null)
                    throw new ArgumentException($"Item id {id} not existing");

                categories.Add(q);
            }
        }
        itemDst.CategoryDbMs = categories;
    }

}