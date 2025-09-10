using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

[Table("Categories", Schema = "supusr")]

public class CategoryDbM : Category, ISeed<CategoryDbM>
{
    [Key]
    public override Guid CategoryId { get; set; }
    [Required]
    public override string CategoryName { get; set; }


    [NotMapped]
    // public override List<ISight> Sights { get; set; }
    public override List<ISight> Sights { get => SightDbM?.ToList<ISight>(); set => new NotImplementedException(); }

    [ForeignKey("SightId")]
    [JsonIgnore]
    public List<SightDbM> SightDbM { get; set; }


    CategoryDbM ISeed<CategoryDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}