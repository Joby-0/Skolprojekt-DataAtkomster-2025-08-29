using Microsoft.Extensions.Logging;

using DbRepos;
using Models.DTO;
using Models;

namespace Services;
    
public class SightServiceDb : ISightService
{
    private readonly SightDbRepos _repo = null;
    private readonly ILogger<SightServiceDb> _logger = null;

    // public Task SeedAsync(string nrOfItems) => _repo.SeedAsync(nrOfItems);
    public Task<ResponsePageDto<ISight>> ReadSightsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _repo.ReadSightsAsync(seeded, flat,filter,pageNumber, pageSize);


    #region constructors
    public SightServiceDb(SightDbRepos repo)
    {
        _repo = repo;
    }
    public SightServiceDb(SightDbRepos repo, ILogger<SightServiceDb> logger):this(repo)
    {
        _logger = logger;
    }
    #endregion
}

