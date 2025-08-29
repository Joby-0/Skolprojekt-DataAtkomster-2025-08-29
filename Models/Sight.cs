namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Sight : ISight, ISeed<Sight>
{
    public Guid SightId { get; set; }
    public string SightName { get; set; }
    public string Description { get; set; }
    public IAddress Address { get; set; }
    public ICategory Category { get; set; }
    public bool Seeded { get; set; }

    public Sight Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        SightId = Guid.NewGuid();
        SightName = seedGenerator.Sight();
        Description = seedGenerator.SightDescription();
        return this;
    }
}