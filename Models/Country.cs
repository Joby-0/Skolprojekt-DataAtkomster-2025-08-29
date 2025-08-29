using Joby.Utilities.SeedGenerator;

namespace Models;

public class Country : ICountry, ISeed<Country>
{
    public Guid CountryId { get; set; }
    public string CountryName { get; set; }


    public bool Seeded { get; set ; }

    public Country Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        CountryId = Guid.NewGuid();

        CountryName = seedGenerator.Country;

        return this;
    }
}