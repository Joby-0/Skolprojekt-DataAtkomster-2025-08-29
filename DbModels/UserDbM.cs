using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class UserDbM : User, ISeed<UserDbM>
{
    [Key]
    public override Guid UserId { get; set; }
    public override string FirstName { get ; set; }
    public override string LastName { get; set; }
    public override string Email { get; set; }

   

    UserDbM ISeed<UserDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}