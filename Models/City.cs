namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class City : ICity, ISeed<City>
{
    public Guid CityId { get; set; }
    public string CityName { get; set; }
    public ICountry Country { get; set; }

    public bool Seeded { get; set; }

    public City Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        CityId = Guid.NewGuid();
        CityName = seedGenerator.City();

        return this;
    }
}