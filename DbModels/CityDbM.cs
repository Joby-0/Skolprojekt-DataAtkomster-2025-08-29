using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class CityDbM : City, ISeed<CityDbM>
{
    [Key]
    public override Guid CityId { get; set; }
    public override string CityName { get ; set; }


    [NotMapped]
    public override ICountry Country { get; set; }
   

    CityDbM ISeed<CityDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}