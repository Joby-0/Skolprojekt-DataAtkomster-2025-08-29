namespace Models.DTO;

public class GstUsrInfoDbDto
{
    public int NrSeededUsers { get; set; } = 0;
    public int NrUnseededUsers { get; set; } = 0;

    public int NrSeededSights { get; set; } = 0;
    public int NrUnseededSights { get; set; } = 0;

    public int NrSeededSightsNoReview { get; set; } = 0;
    public int NrSeededReviews { get; set; } = 0;

    public int NrSeededCountries { get; set; } = 0;
    public int NrUnseededCountries { get; set; } = 0;

    public int NrSeededCities { get; set; } = 0;
    public int NrUnseededCities { get; set; } = 0;

}

public class GstUsrInfoFriendsDto
{
    public string Country { get; set; } = null;
    public string City { get; set; } = null;
    public int NrFriends { get; set; } = 0;
}

public class GstUsrInfoPetsDto
{
    public string Country { get; set; } = null;
    public string City { get; set; } = null;
    public int NrPets { get; set; } = 0;
}

public class GstUsrInfoQuotesDto
{
    public string Author { get; set; } = null;
    public int NrQuotes { get; set; } = 0;
}

public class GstUsrInfoAllDto
{
    public GstUsrInfoDbDto Db { get; set; } = null;

}


