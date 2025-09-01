using Microsoft.Extensions.Logging;

using DbRepos;

namespace Services;
    
public class SightService : ISightService
{
    private readonly SightDbRepos _repo = null;
    private readonly ILogger<SightService> _logger = null;

    public Task SeedAsync() => _repo.SeedAsync();

    #region constructors
    public SightService(SightDbRepos repo)
    {
        _repo = repo;
    }
    public SightService(SightDbRepos repo, ILogger<SightService> logger):this(repo)
    {
        _logger = logger;
    }
    #endregion
}

