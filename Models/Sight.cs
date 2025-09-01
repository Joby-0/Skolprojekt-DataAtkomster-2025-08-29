namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Sight : ISight, ISeed<Sight>
{
    public virtual Guid SightId { get; set; }
    public virtual string SightName { get; set; }
    public virtual string Description { get; set; }
    public virtual IAddress Address { get; set; } = null;

    //en sight kan ha flera categories eller ingen
    public virtual List<ICategory> Categories { get; set; } = null;
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