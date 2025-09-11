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

    
    public SightDbRepos(ILogger<SightDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }
}