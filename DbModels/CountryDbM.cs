using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class CountryDbM : Country, ISeed<CountryDbM>
{
    [Key]
    public override Guid CountryId { get; set; }
    public override string CountryName { get ; set; }


   

    CountryDbM ISeed<CountryDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}