using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Newtonsoft.Json;


using Models;
using Models.DTO;

namespace DbModels;

[Table("Users", Schema = "supusr")]

public class UserDbM : User, ISeed<UserDbM>
{
    [Key]
    public override Guid UserId { get; set; }
    [Required]
    public override string FirstName { get; set; }
    [Required]
    public override string LastName { get; set; }
    [Required]
    public override string Email { get; set; }

    [NotMapped]
    public override List<IReview> Reviews { get => ReviewDbMs?.ToList<IReview>(); set => new NotImplementedException(); }

    [JsonIgnore]
    public List<ReviewDbM> ReviewDbMs { get; set; }



    UserDbM ISeed<UserDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }

    #region Update from DTO
    public UserDbM UpdateFromDTO(UserCuDto org)
    {
        FirstName = org.FirstName;
        LastName = org.LastName;
        Email = org.Email;

        return this;
    }
    #endregion

    #region constructors
    public UserDbM() { }
    public UserDbM(UserCuDto org)
    {
        UserId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion

}