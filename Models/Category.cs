namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Category : ICategory, ISeed<Category>
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public ISight Sight { get; set; }
    public bool Seeded { get; set; }

    public Category Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        CategoryId = Guid.NewGuid();
        CategoryName = seedGenerator.RandomCategory(); 
        return this;
    }
}