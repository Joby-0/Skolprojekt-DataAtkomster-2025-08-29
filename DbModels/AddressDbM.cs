using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;
[Table("Addresses", Schema = "supusr")]

public class AddressDbM : Address, ISeed<AddressDbM>, IEquatable<AddressDbM>
{
    [Key]
    public override Guid AddressId { get; set; }
    [Required]
    public override string Street { get; set; }
    [Required]
    public override int ZipCode { get; set; }

    [JsonIgnore]
    public Guid? CityId { get; set; }

    [NotMapped]
    public override ICity City { get => CityDbM; set => new NotImplementedException(); }

    [JsonIgnore]
    [Required]
    [ForeignKey("CityId")]
    public CityDbM CityDbM { get; set; }

    AddressDbM ISeed<AddressDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }

    #region implementing IEquatable
    public bool Equals(AddressDbM other) => (other != null) && ((Street, ZipCode, City) ==
        (other.Street, other.ZipCode, other.City));

    public override bool Equals(object obj) => Equals(obj as AddressDbM);
    public override int GetHashCode() => (Street, ZipCode, City).GetHashCode();
    #endregion
}