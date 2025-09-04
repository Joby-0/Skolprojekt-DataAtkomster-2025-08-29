using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class SightDbM : Sight, ISeed<SightDbM>
{
    [Key]
    public override Guid SightId { get; set; }
    [Required]
    public override string SightName { get; set; }

    [NotMapped]
    public override IAddress Address { get; set; }

    [NotMapped]
    public override List<ICategory> Categories { get; set; }
   

    SightDbM ISeed<SightDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}