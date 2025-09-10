using Microsoft.Extensions.Logging;

using DbRepos;
using Models.DTO;
using Models;

namespace Services;
    
public class SightServiceDb : ISightService
{
    private readonly SightDbRepos _repo = null;
    private readonly ILogger<SightServiceDb> _logger = null;

    public Task SeedAsync() => _repo.SeedAsync();
    public Task<ResponsePageDto<ISight>> ReadSightsAsync() => _repo.ReadSightsAsync();


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

