namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class User : IUser, ISeed<User>
{
    public virtual Guid UserId { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }
    public virtual bool Seeded { get; set; }

    public virtual List<IReview> Reviews { get; set; }

    public User Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        UserId = Guid.NewGuid();
        FirstName = seedGenerator.FirstName;
        LastName = seedGenerator.LastName;
        Email = seedGenerator.Email(FirstName, LastName);

        return this;
    }
}