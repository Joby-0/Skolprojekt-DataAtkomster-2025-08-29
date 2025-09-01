using System.ComponentModel.DataAnnotations;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class SightDbM : Sight, ISeed<SightDbM>
{
    [Key]
    public override Guid SightId { get; set; }

    SightDbM ISeed<SightDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}