using Microsoft.Extensions.Logging;

using DbRepos;

namespace Services;
    
public class UserServiceDb : IUserService
{
    private readonly SightDbRepos _repo = null;
    private readonly ILogger<UserServiceDb> _logger = null;

    // public Task SeedAsync(string nrOfItems) => _repo.SeedAsync(nrOfItems);

    #region constructors
    public UserServiceDb(SightDbRepos repo)
    {
        _repo = repo;
    }
    public UserServiceDb(SightDbRepos repo, ILogger<UserServiceDb> logger):this(repo)
    {
        _logger = logger;
    }
    #endregion
}

