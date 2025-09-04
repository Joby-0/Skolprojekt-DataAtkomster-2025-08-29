using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

[Table("Sights", Schema = "supusr")]
public class SightDbM : Sight, ISeed<SightDbM>
{
    [Key]
    public override Guid SightId { get; set; }
    [Required]
    public override string SightName { get; set; }

    [JsonIgnore]
    public Guid? AddressId { get; set; }
    // public Guid? CategoryId { get; set; }

    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => new NotImplementedException(); }

    [ForeignKey("AddressId")]
    [JsonIgnore]
    public AddressDbM AddressDbM { get; set; }

    [NotMapped]
    public override List<ICategory> Categories { get => CategoryDbM?.ToList<ICategory>(); set => new NotImplementedException(); }

    // [ForeignKey("CategoryId")]
    [JsonIgnore]
    public List<CategoryDbM> CategoryDbM { get; set; }



    SightDbM ISeed<SightDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}