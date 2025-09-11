using Microsoft.Extensions.Logging;

using DbRepos;
using Models;
using Models.DTO;

namespace Services;
    
public class UserServiceDb : IUserService
{
    private readonly UserDbRepos _repo = null;
    private readonly ILogger<UserServiceDb> _logger = null;

    public Task<ResponsePageDto<IUser>> ReadUsersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _repo.ReadUsersAsync(seeded, flat,filter,pageNumber,pageSize);

    public Task<ResponseItemDto<IUser>> ReadUserAsync(Guid id, bool flat) => _repo.ReadUserAsync(id, flat);


    #region constructors
    public UserServiceDb(UserDbRepos repo)
    {
        _repo = repo;
    }
    public UserServiceDb(UserDbRepos repo, ILogger<UserServiceDb> logger):this(repo)
    {
        _logger = logger;
    }
    #endregion
}

