namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Address : IAddress, ISeed<Address>, IEquatable<Address>
{
    public virtual Guid AddressId { get; set; }
    public virtual string Street { get; set; }
    public virtual int ZipCode { get; set; }
    public virtual ICity City { get; set; }
    public bool Seeded { get; set; }

    public Address Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        AddressId = Guid.NewGuid();
        Street = seedGenerator.StreetAddress();
        ZipCode = seedGenerator.ZipCode;

        return this;
    }

    #region implementing IEquatable

    public bool Equals(Address other) => (other != null) && ((this.Street, this.ZipCode, this.City) ==
        (other.Street, other.ZipCode, other.City));

    public override bool Equals(object obj) => Equals(obj as Address);
    public override int GetHashCode() => (Street, ZipCode, City).GetHashCode();

    #endregion
}

