namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class User : IUser, ISeed<User>
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Seeded { get; set; }

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