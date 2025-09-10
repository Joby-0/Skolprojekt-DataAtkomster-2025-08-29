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

public class SightDbRepos
{
    private readonly ILogger<SightDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync()
    {
        var seeder = new SeedGenerator();

        var cities = seeder.UniqueItemsToList<CityDbM>(300);
        var countries = seeder.UniqueItemsToList<CountryDbM>(100);
        var addresses = seeder.UniqueItemsToList<AddressDbM>(1000);
        var sights = seeder.ItemsToList<SightDbM>(1000);

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
        }
        _dbContext.Sights.AddRange(sights);

        await _dbContext.SaveChangesAsync();
    }
    public async Task<ResponsePageDto<ISight>> ReadSightsAsync()
    {
        IQueryable<SightDbM> query = _dbContext.Sights;

        var ret = new ResponsePageDto<ISight>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            DbItemsCount = await query.CountAsync(),
            PageItems = await query.ToListAsync<ISight>(),
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