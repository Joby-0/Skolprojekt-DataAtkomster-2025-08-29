using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;
using Newtonsoft.Json;


namespace DbModels;
[Table("Countries", Schema = "supusr")]

public class CountryDbM : Country, ISeed<CountryDbM>, IEquatable<CountryDbM>
{
    [Key]
    public override Guid CountryId { get; set; }
    
    [Required]
    public override string CountryName { get; set; }




    CountryDbM ISeed<CountryDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
    #region implementing IEquatable
    public bool Equals(CountryDbM other) => (other != null) && ((CountryName) ==
        (other.CountryName));

    public override bool Equals(object obj) => Equals(obj as CountryDbM);
    public override int GetHashCode() => (CountryName).GetHashCode();
    #endregion
}