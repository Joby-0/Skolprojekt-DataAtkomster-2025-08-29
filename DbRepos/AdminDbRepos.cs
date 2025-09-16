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

public class AdminDbRepos
{
    private readonly ILogger<AdminDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync(string nrOfItems)
    {
        int nrOfItemsInt = int.Parse(nrOfItems);
        var seeder = new SeedGenerator();

        var users = seeder.ItemsToList<UserDbM>(50);
        var reviews = seeder.ItemsToList<ReviewDbM>(nrOfItemsInt * 15);
        var cities = seeder.UniqueItemsToList<CityDbM>(100);
        var countries = seeder.UniqueItemsToList<CountryDbM>(25);
        var addresses = seeder.UniqueItemsToList<AddressDbM>(nrOfItemsInt);
        var sights = seeder.ItemsToList<SightDbM>(nrOfItemsInt);

        foreach (var review in reviews)
        {
            review.UserDbM = seeder.FromList(users);
        }

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
            sight.CategoryDbMs = seeder.ItemsToList<CategoryDbM>(seeder.Next(1, 5));
            sight.ReviewDbMs = seeder.UniqueItemsPickedFromList(seeder.Next(0, 20), reviews);

        }
        _dbContext.Sights.AddRange(sights);

        await _dbContext.SaveChangesAsync();
    }


    public async Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync() => await DbInfo();

    private async Task<ResponseItemDto<GstUsrInfoAllDto>> DbInfo()
    {
        var info = new GstUsrInfoAllDto();
        info.Db = new GstUsrInfoDbDto
        {
            NrSeededSights = await _dbContext.Sights.Where(s => s.Seeded).CountAsync(),
            // NrUnseededSights = await _dbContext.Sights.Where(s => !s.Seeded).CountAsync(),
            NrSeededSightsNoReview = await _dbContext.Sights.Where(s => !s.ReviewDbMs.Any()).CountAsync(),

            NrSeededReviews = await _dbContext.Reviews.Where(r => r.Seeded).CountAsync(),

            NrSeededCountries = await _dbContext.Countries.Where(c => c.Seeded).CountAsync(),
            // NrUnseededCountries = await _dbContext.Countries.Where(c => !c.Seeded).CountAsync(),

            NrSeededCities = await _dbContext.Cities.Where(c => c.Seeded).CountAsync(),
            // NrUnseededCities = await _dbContext.Cities.Where(c => !c.Seeded).CountAsync(),

            NrSeededUsers = await _dbContext.Users.Where(u => u.Seeded).CountAsync(),
            // NrUnseededUsers = await _dbContext.Users.Where(u => !u.Seeded).CountAsync(),
        };

        return new ResponseItemDto<GstUsrInfoAllDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = info
        };
    }

    public async Task RemoveSeedAsync()
    {
        _dbContext.RemoveRange(_dbContext.Countries.Where(c => c.Seeded == true));
        _dbContext.RemoveRange(_dbContext.Cities.Where(c => c.Seeded == true));
        _dbContext.RemoveRange(_dbContext.Addresses.Where(a => a.Seeded == true));
        _dbContext.RemoveRange(_dbContext.Categories.Where(c => c.Seeded == true));
        _dbContext.RemoveRange(_dbContext.Reviews.Where(r => r.Seeded == true));
        _dbContext.RemoveRange(_dbContext.Users.Where(u => u.Seeded == true));
        _dbContext.RemoveRange(_dbContext.Sights.Where(s => s.Seeded == true));

        await _dbContext.SaveChangesAsync();
    }
    public AdminDbRepos(ILogger<AdminDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }
}