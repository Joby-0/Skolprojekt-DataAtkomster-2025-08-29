using Joby.Utilities.SeedGenerator;

namespace Models;

public class Country : ICountry, ISeed<Country>, IEquatable<Country>
{
    public virtual Guid CountryId { get; set; }
    public virtual string CountryName { get; set; }


    public bool Seeded { get; set; }

    public Country Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        CountryId = Guid.NewGuid();

        CountryName = seedGenerator.Country;

        return this;
    }
    
    #region implementing IEquatable
    public bool Equals(Country other) => (other != null) && ((CountryName) ==
        (other.CountryName));

    public override bool Equals(object obj) => Equals(obj as Country);
    public override int GetHashCode() => (CountryName).GetHashCode();
    #endregion
}