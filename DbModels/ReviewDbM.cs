using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class ReviewDbM : Review, ISeed<ReviewDbM>
{
    [Key]
    public override Guid ReviewId { get; set; }
    public override int Rating { get; set; }
    public override string Comment { get; set; }
    public override DateTime Created_at { get; set; }

    [NotMapped]
    public override ISight Sight { get; set; }
    [NotMapped]
    public override IUser User { get; set; }



    ReviewDbM ISeed<ReviewDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}