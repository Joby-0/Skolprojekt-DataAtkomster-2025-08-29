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

public class UserDbRepos
{
    private readonly ILogger<UserDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;


    public async Task<ResponsePageDto<IUser>> ReadUsersAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize)
    {
        filter ??= "";
        IQueryable<UserDbM> query;
        if (flat)
        {
            query = _dbContext.Users.AsNoTracking();
        }
        else
        {
            query = _dbContext.Users.Include(u => u.ReviewDbMs).AsNoTracking();

        }

        var ret = new ResponsePageDto<IUser>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            DbItemsCount = await query.CountAsync(),

            // .Where(i => (i.Seeded == seeded)),

            PageItems = await query

            // .Where(i => (i.Seeded == seeded))


            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync<IUser>(),

            PageNr = pageNumber,
            PageSize = pageSize


        };
        return ret;
    }

    public async Task<ResponseItemDto<IUser>> ReadUserAsync(Guid id, bool flat)
    {
        IQueryable<UserDbM> query;
        if (flat)
        {
            query = _dbContext.Users.AsNoTracking();
        }
        else
        {
            query = _dbContext.Users.AsNoTracking();
        }
        var ret = new ResponseItemDto<IUser>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = await query.FirstOrDefaultAsync(i => i.UserId == id)


        };
        return ret;

    }

    public async Task<ResponseItemDto<IUser>> DeleteUserAsync(Guid id)
    {
        var query = _dbContext.Users
            .Where(i => i.UserId == id);
        var item = await query.FirstOrDefaultAsync<UserDbM>();

        _dbContext.Users.Remove(item);
        await _dbContext.SaveChangesAsync();
        return new ResponseItemDto<IUser>()
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif
            Item = item
        };
    }


    public UserDbRepos(ILogger<UserDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }
}