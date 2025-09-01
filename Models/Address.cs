namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Address : IAddress, ISeed<Address>
{
    public virtual Guid AddressId { get; set; }
    public virtual string Street { get; set; }
    public virtual int ZipCode { get; set; }
    public virtual City City { get; set; }
    public bool Seeded { get; set; }

    public Address Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        AddressId = Guid.NewGuid();
        Street = seedGenerator.StreetAddress();
        ZipCode = seedGenerator.ZipCode;

        return this;
    }
}