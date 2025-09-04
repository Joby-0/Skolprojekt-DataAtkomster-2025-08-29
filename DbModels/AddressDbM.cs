using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class AddressDbM : Address, ISeed<AddressDbM>
{
    [Key]
    public override Guid AddressId { get; set; }
    [Required]
    public override string Street { get; set; }
    [Required]
    public override int ZipCode { get; set; }

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
}