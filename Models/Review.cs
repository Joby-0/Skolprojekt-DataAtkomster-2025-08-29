namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Review : IReview, ISeed<Review>
{
    public virtual Guid ReviewId { get; set; }
    public virtual int Rating { get; set; }
    public virtual string Comment { get; set; } = null;
    public virtual DateTime Created_at { get; set; }
    public virtual ISight Sight { get; set; }
    public virtual IUser User { get; set; }
    public bool Seeded { get; set; }

    public Review Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        ReviewId = Guid.NewGuid();
        Rating = seedGenerator.Next(0, 6);
        Comment = seedGenerator.RandomComment();
        Created_at = seedGenerator.DateAndTime(1979, 2026);
        return this;
    }
}