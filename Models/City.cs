namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class City : ICity, ISeed<City>
{
    public virtual Guid CityId { get; set; }
    public virtual string CityName { get; set; }
    public virtual Country Country { get; set; }

    public bool Seeded { get; set; }

    public City Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        CityId = Guid.NewGuid();
        CityName = seedGenerator.City();

        return this;
    }
}