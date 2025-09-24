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
using System.Data.Common;
using MySqlConnector;
using Npgsql;
using Microsoft.Data.SqlClient;

public class AdminDbRepos
{
    private readonly ILogger<AdminDbRepos> _logger;
    // private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync(string nrOfItems)
    {
        await RemoveSeedAsync();
        int nrOfItemsInt = int.Parse(nrOfItems);
        var seeder = new SeedGenerator();

        var users = seeder.ItemsToList<UserDbM>(nrOfItemsInt);
        var reviews = seeder.ItemsToList<ReviewDbM>(nrOfItemsInt * 10);
        var cities = seeder.UniqueItemsToList<CityDbM>(seeder.Next(100, nrOfItemsInt));
        var countries = seeder.UniqueItemsToList<CountryDbM>(seeder.Next(5, nrOfItemsInt));
        var addresses = seeder.UniqueItemsToList<AddressDbM>(nrOfItemsInt);
        var sights = seeder.ItemsToList<SightDbM>(nrOfItemsInt);

        foreach (var review in reviews)
        {
            review.UserDbM = seeder.FromList(users);
        }

        foreach (var city in cities)
        {
            city.CountryDbM = seeder.FromList(countries);
        }

        foreach (var address in addresses)
        {
            address.CityDbM = seeder.FromList(cities);
        }


        foreach (var sight in sights)
        {
            sight.AddressDbM = seeder.FromList(addresses);
            sight.CategoryDbMs = seeder.ItemsToList<CategoryDbM>(seeder.Next(1, 5));
            sight.ReviewDbMs = seeder.UniqueItemsPickedFromList(seeder.Next(0, 20), reviews);

        }
        _dbContext.Sights.AddRange(sights);

        await _dbContext.SaveChangesAsync();
    }


    public async Task<ResponseItemDto<GstUsrInfoAllDto>> InfoAsync() => await DbInfo();

    private async Task<ResponseItemDto<GstUsrInfoAllDto>> DbInfo()
    {
        var info = new GstUsrInfoAllDto();
        info.Db = await _dbContext.InfoDbView.FirstAsync();

        return new ResponseItemDto<GstUsrInfoAllDto>
        {
#if DEBUG
            ConnectionString = _dbContext.dbConnection,
#endif

            Item = info
        };
    }

    public async Task<GstUsrInfoDbDto> RemoveSeedAsync()
    {
        bool seeded = true;
        // Create parameters based on database provider
        var connection = _dbContext.Database.GetDbConnection();
        using var command = connection.CreateCommand();
        command.CommandType = CommandType.StoredProcedure;

        List<DbParameter> parameters;
        if (connection is MySqlConnection)
        {
            command.CommandText = "supusr_spDeleteAll";
            parameters = new List<DbParameter>
            {
                new MySqlParameter("seededParam", seeded),
                new MySqlParameter("nrFriendsAffected", MySqlDbType.Int32) { Direction = ParameterDirection.Output },
                new MySqlParameter("nrAddressesAffected", MySqlDbType.Int32) { Direction = ParameterDirection.Output },
                new MySqlParameter("nrPetsAffected", MySqlDbType.Int32) { Direction = ParameterDirection.Output },
                new MySqlParameter("nrQuotesAffected", MySqlDbType.Int32) { Direction = ParameterDirection.Output }
            };
        }
        else if (connection is NpgsqlConnection)
        {
            // PostgreSQL parameters - call as function returning table
            command.CommandText = "SELECT nrFriendsAffected, nrAddressesAffected, nrPetsAffected, nrQuotesAffected FROM supusr.\"spDeleteAll\"(@seededParam)";
            command.CommandType = CommandType.Text;
            parameters =
            [
                new NpgsqlParameter("seededParam", seeded),
                new NpgsqlParameter("nrFriendsAffected", NpgsqlTypes.NpgsqlDbType.Integer) { Direction = ParameterDirection.Output },
                new NpgsqlParameter("nrAddressesAffected", NpgsqlTypes.NpgsqlDbType.Integer) { Direction = ParameterDirection.Output },
                new NpgsqlParameter("nrPetsAffected", NpgsqlTypes.NpgsqlDbType.Integer) { Direction = ParameterDirection.Output },
                new NpgsqlParameter("nrQuotesAffected", NpgsqlTypes.NpgsqlDbType.Integer) { Direction = ParameterDirection.Output }
            ];
        }
        else
        {
            // SQL Server parameters (default)
            command.CommandText = "supusr.spDeleteAll";
            parameters = new List<DbParameter>
            {
                new SqlParameter("seededParam", seeded),
                new SqlParameter("nrUsersAffected", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("nrSightsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("nrReviewsAffected", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("nrCountriesAffected", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("nrCitiesAffected", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("nrCategoriesAffected", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("nrAddressesAffected", SqlDbType.Int) { Direction = ParameterDirection.Output },

            };
        }
        command.Parameters.AddRange(parameters.ToArray());

        // _dbContext.RemoveRange(_dbContext.Countries.Where(c => c.Seeded == true));
        // _dbContext.RemoveRange(_dbContext.Cities.Where(c => c.Seeded == true));
        // _dbContext.RemoveRange(_dbContext.Addresses.Where(a => a.Seeded == true));
        // _dbContext.RemoveRange(_dbContext.Categories.Where(c => c.Seeded == true));
        // _dbContext.RemoveRange(_dbContext.Reviews.Where(r => r.Seeded == true));
        // _dbContext.RemoveRange(_dbContext.Users.Where(u => u.Seeded == true));
        // _dbContext.RemoveRange(_dbContext.Sights.Where(s => s.Seeded == true));

        await _dbContext.SaveChangesAsync();
        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();
            

        if (connection is NpgsqlConnection)
        {
            // in postgresql, execute a procedure (a function) cannot return a dataset and have output parameters
            // therefore I execute the command without expecting a result set
            await command.ExecuteScalarAsync();
            return null;
        }
        else
        {
            // Execute the stored procedure and get the result set
            using var reader = await command.ExecuteReaderAsync();

            // map reader result into GstUsrInfoDbDto result_set
            GstUsrInfoDbDto result_set = null;
            if (reader.HasRows)
            {
                // Read the first result set which should be InfoDbView
                await reader.ReadAsync();

                result_set = new GstUsrInfoDbDto
                {
                    // Populate properties from the reader
                    NrSeededUsers = Convert.ToInt32(reader["NrSeededUsers"]),
                    NrUnseededUsers = Convert.ToInt32(reader["NrUnseededUsers"]),
                    NrSeededSights = Convert.ToInt32(reader["NrSeededSights"]),
                    NrUnseededSights = Convert.ToInt32(reader["NrUnseededSights"]),
                    NrSeededSightsNoReview = Convert.ToInt32(reader["NrSeededSightsNoReview"]),
                    NrSeededReviews = Convert.ToInt32(reader["NrSeededReviews"]),
                    NrSeededCities = Convert.ToInt32(reader["NrSeededCities"]),
                    NrUnseededCities = Convert.ToInt32(reader["NrUnseededCities"]),
                    NrSeededCountries = Convert.ToInt32(reader["NrSeededCountries"]),
                    NrUnseededCountries = Convert.ToInt32(reader["NrUnseededCountries"])


                };
            }
            await reader.CloseAsync();
            // result_set can now be accessed - not used in this example
            return result_set;
        }

        
    }
    public AdminDbRepos(ILogger<AdminDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        // _encryptions = encryptions;
        _dbContext = context;
    }
}