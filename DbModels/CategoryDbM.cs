using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class CategoryDbM : Category, ISeed<CategoryDbM>
{
    [Key]
    public override Guid CategoryId { get; set; }
    [Required]
    public override string CategoryName { get; set; }


    [NotMapped]
    public override List<ISight> Sights { get; set; }
   

    CategoryDbM ISeed<CategoryDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}