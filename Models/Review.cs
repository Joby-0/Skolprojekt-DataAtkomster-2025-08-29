namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Review : IReview, ISeed<Review>
{
    public Guid ReviewId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Created_at { get; set; }
    public ISight Sight { get; set; }
    public IUser User { get; set; }
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