USE [sql-Sights-Johan_Bylander]
GO

--create a schemas
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'gstusr')
    EXEC('CREATE SCHEMA gstusr');
GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'usr')
    EXEC('CREATE SCHEMA usr');
GO

--create a view that gives overview of the database content
CREATE OR ALTER VIEW gstusr.vwInfoDb AS
    SELECT (SELECT COUNT(*) FROM supusr.Users WHERE Seeded = 1) as NrSeededUsers, 
        (SELECT COUNT(*) FROM supusr.Users WHERE Seeded = 0) as NrUnseededUsers,
        (SELECT COUNT(*) FROM supusr.Sights WHERE Seeded = 1) as NrSeededSights, 
        (SELECT COUNT(*) FROM supusr.Sights WHERE Seeded = 0) as NrUnseededSights,
        (SELECT COUNT(*) FROM supusr.Sights WHERE Seeded = 1) as NrSeededSightsNoReview, 
        (SELECT COUNT(*) FROM supusr.Reviews WHERE Seeded = 1) as NrSeededReviews,
        (SELECT COUNT(*) FROM supusr.Countries WHERE Seeded = 1) as NrSeededCountries,
        (SELECT COUNT(*) FROM supusr.Countries WHERE Seeded = 0) as NrUnseededCountries,
        (SELECT COUNT(*) FROM supusr.Cities WHERE Seeded = 1) as NrSeededCities,
        (SELECT COUNT(*) FROM supusr.Cities WHERE Seeded = 0) as NrUnseededCities;


GO

CREATE OR ALTER VIEW supusr.vwAllSights AS
    SELECT s.SightId AS SightId,  s.SightName AS SightName, a.Street AS Street, ci.CityName AS City, co.CountryName AS Country From supusr.Sights s

    JOIN supusr.Addresses a ON s.AddressId = a.AddressId
    JOIN supusr.Cities ci ON a.CityId = ci.CityId
    JOIN supusr.Countries co ON ci.CountryId = co.CountryId
    GROUP BY s.SightName, a.Street, ci.CityName, co.CountryName, s.SightId


GO


--create the DeleteAll procedure
CREATE OR ALTER PROC supusr.spDeleteAll
    @seededParam BIT = 1,

    @nrUsersAffected INT OUTPUT,
    @nrSightsAffected INT OUTPUT,
    @nrReviewsAffected INT OUTPUT,
    @nrCountriesAffected INT OUTPUT,
    @nrCitiesAffected INT OUTPUT,
    @nrCategoriesAffected INT OUTPUT,
    @nrAddressesAffected INT OUTPUT



    
    AS

    SET NOCOUNT ON;

    SELECT  @nrUsersAffected = COUNT(*) FROM supusr.Users WHERE Seeded = @seededParam;
    SELECT  @nrSightsAffected = COUNT(*) FROM supusr.Sights WHERE Seeded = @seededParam;
    SELECT  @nrReviewsAffected = COUNT(*) FROM supusr.Reviews WHERE Seeded = @seededParam;
    SELECT  @nrCountriesAffected = COUNT(*) FROM supusr.Countries WHERE Seeded = @seededParam;
    SELECT  @nrCitiesAffected = COUNT(*) FROM supusr.Cities WHERE Seeded = @seededParam;
    SELECT  @nrAddressesAffected = COUNT(*) FROM supusr.Addresses WHERE Seeded = @seededParam;
    SELECT  @nrCategoriesAffected = COUNT(*) FROM supusr.Categories WHERE Seeded = @seededParam;


    DELETE FROM supusr.Users WHERE Seeded = @seededParam;
    DELETE FROM supusr.Sights WHERE Seeded = @seededParam;
    DELETE FROM supusr.Reviews WHERE Seeded = @seededParam;

    DELETE FROM supusr.Addresses WHERE Seeded = @seededParam;
    DELETE FROM supusr.Cities WHERE Seeded = @seededParam;
    DELETE FROM supusr.Countries WHERE Seeded = @seededParam;

    DELETE FROM supusr.Categories WHERE Seeded = @seededParam;

    





    --throw our own error
    --;THROW 999999, 'Error occurred in supusr.spDeleteAll', 1

    SELECT * FROM gstusr.vwInfoDb;
GO



