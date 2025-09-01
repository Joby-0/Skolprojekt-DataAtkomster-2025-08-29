namespace Models;

using System;
using Joby.Utilities.SeedGenerator;

public class Category : ICategory, ISeed<Category>
{
    public virtual Guid CategoryId { get; set; }
    public virtual string CategoryName { get; set; }

    //en categori kan ha flera sights eller ingen
    public virtual List<ISight> Sights { get; set; } = null;
    public bool Seeded { get; set; }

    public Category Seed(SeedGenerator seedGenerator)
    {
        Seeded = true;
        CategoryId = Guid.NewGuid();
        CategoryName = seedGenerator.RandomCategory(); 
        return this;
    }
}