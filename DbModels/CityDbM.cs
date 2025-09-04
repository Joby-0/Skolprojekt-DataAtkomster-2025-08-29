using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class CityDbM : City, ISeed<CityDbM>, IEquatable<CityDbM>
{
    [Key]
    public override Guid CityId { get; set; }
    [Required]
    public override string CityName { get; set; }

    [JsonIgnore]
    public Guid? CountryId { get; set; }


    [NotMapped]
    public override ICountry Country { get => CountryDbM; set => new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    [ForeignKey("CountryId")]
    public CountryDbM CountryDbM { get; set; }

    CityDbM ISeed<CityDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }

    #region implementing IEquatable
    public bool Equals(CityDbM other) => (other != null) && ((CityName) ==
        (other.CityName));

    public override bool Equals(object obj) => Equals(obj as CityDbM);
    public override int GetHashCode() => (CityName).GetHashCode();
    #endregion
}