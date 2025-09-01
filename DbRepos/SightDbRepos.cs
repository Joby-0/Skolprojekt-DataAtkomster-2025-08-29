namespace DbRepos;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using DbModels;
using DbContext;
using Configuration;
using Joby.Utilities.SeedGenerator;
public class SightDbRepos
{
    private readonly ILogger<SightDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync()
    {
        
        var seeder = new SeedGenerator();
        
        var sights = seeder.ItemsToList<SightDbM>(100);
        _dbContext.Sights.AddRange(sights);
        
        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

    public SightDbRepos(ILogger<SightDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }
}