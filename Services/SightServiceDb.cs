using Microsoft.Extensions.Logging;

using DbRepos;

namespace Services;
    
public class SightServiceDb : ISightService
{
    private readonly SightDbRepos _repo = null;
    private readonly ILogger<SightServiceDb> _logger = null;

    public Task SeedAsync() => _repo.SeedAsync();

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

