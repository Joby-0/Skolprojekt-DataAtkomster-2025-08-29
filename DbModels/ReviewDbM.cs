using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class ReviewDbM : Review, ISeed<ReviewDbM>
{
    [Key]
    public override Guid ReviewId { get; set; }
    [Required]
    public override int Rating { get; set; }
    public override string Comment { get; set; }
    public override DateTime Created_at { get; set; }

    [JsonIgnore]
    public Guid? SightId { get; set; }

    [JsonIgnore]
    public Guid? UserId { get; set; }

    [NotMapped]
    public override ISight Sight { get => SightDbM; set => new NotImplementedException(); }        

    [ForeignKey("SightId")]
    [JsonIgnore]
    [Required]
    public SightDbM SightDbM { get; set; }

    [NotMapped]
    public override IUser User { get => UserDbM; set => new NotImplementedException(); }   

    [ForeignKey("UserId")]
    [JsonIgnore]
    [Required]
    public UserDbM UserDbM { get; set; }



    ReviewDbM ISeed<ReviewDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}