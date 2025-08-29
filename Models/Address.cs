namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Address : IAddress, ISeed<Address>
{
    public Guid AddressId { get; set; }
    public string Street { get; set; }
    public int ZipCode { get; set; }
    public ICity City { get; set; }
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