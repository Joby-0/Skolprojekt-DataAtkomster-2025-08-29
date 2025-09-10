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

    public async Task SeedAsync(string nrOfItems)
    {
        int nrOfItemsInt = int.Parse(nrOfItems);
        var seeder = new SeedGenerator();


        var cities = seeder.UniqueItemsToList<CityDbM>(nrOfItemsInt / 3);
        var countries = seeder.UniqueItemsToList<CountryDbM>(nrOfItemsInt / 10);
        var addresses = seeder.UniqueItemsToList<AddressDbM>(nrOfItemsInt);
        var sights = seeder.ItemsToList<SightDbM>(nrOfItemsInt);

        foreach (var city in cities)
        {
            city.CountryDbM = seeder.FromList(countries);
        }

        foreach (var address in addresses)
        {
            address.CityDbM = seeder.FromList(cities);
        }


        foreach (var sight in sights)
        {
            sight.AddressDbM = seeder.FromList(addresses);
            sight.CategoryDbM = seeder.ItemsToList<CategoryDbM>(seeder.Next(1, 5));
        }
        _dbContext.Sights.AddRange(sights);

        await _dbContext.SaveChangesAsync();
    }
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
                .Include(s => s.CategoryDbM)
                .AsNoTracking();
        }

        var ret = new ResponsePageDto<ISight>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            DbItemsCount = await query.CountAsync(),
            PageItems = await query
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