using Microsoft.Extensions.Logging;

using DbRepos;
using Models.DTO;
using Models;

namespace Services;
    
public class AdminServiceDb : IAdminService
{
    private readonly AdminDbRepos _repo = null;
    private readonly ILogger<AdminServiceDb> _logger = null;


    public Task SeedAsync(string nrOfItems) => _repo.SeedAsync(nrOfItems);

    public Task<ResponseItemDto<GstUsrInfoAllDto>> DbInfo() => _repo.InfoAsync();

    public Task RemoveSeedAsync() => _repo.RemoveSeedAsync();







    #region constructors
    public AdminServiceDb(AdminDbRepos repo)
    {
        _repo = repo;
    }
    public AdminServiceDb(AdminDbRepos repo, ILogger<AdminServiceDb> logger):this(repo)
    {
        _logger = logger;
    }
    #endregion
}

