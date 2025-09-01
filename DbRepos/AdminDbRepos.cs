using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using DbModels;
using DbContext;
using Configuration;
using Joby.Utilities.SeedGenerator;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync()
    {
        //Create a seeder
        var seeder = new SeedGenerator();
        var sights = seeder.ItemsToList<SightDbM>(100);
        _dbContext.Sights.AddRange(sights);
        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }
}
