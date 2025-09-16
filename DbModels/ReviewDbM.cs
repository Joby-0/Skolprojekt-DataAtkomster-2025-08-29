using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

[Table("Reviews", Schema = "supusr")]

public class ReviewDbM : Review, ISeed<ReviewDbM>, IEquatable<ReviewDbM>
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
    public Guid?UserId { get; set; }

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
        #region implementing IEquatable
    public bool Equals(ReviewDbM other) => (other != null) && ((Rating, Comment, Created_at, User) ==
        (other.Rating, other.Comment, other.Created_at,other.User));

    public override bool Equals(object obj) => Equals(obj as ReviewDbM);
    public override int GetHashCode() => (Rating, Comment, Created_at, User).GetHashCode();
    #endregion
}