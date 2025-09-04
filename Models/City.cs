namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class City : ICity, ISeed<City>, IEquatable<City>
{
    public virtual Guid CityId { get; set; }
    public virtual string CityName { get; set; }
    public virtual ICountry Country { get; set; }

    public bool Seeded { get; set; }

    public City Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        CityId = Guid.NewGuid();
        CityName = seedGenerator.City();

        return this;
    }
    #region implementing IEquatable
    public bool Equals(City other) => (other != null) && ((CityName) ==
        (other.CityName));

    public override bool Equals(object obj) => Equals(obj as City);
    public override int GetHashCode() => (CityName).GetHashCode();
    #endregion
}