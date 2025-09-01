using System.ComponentModel.DataAnnotations;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class AddressDbM : Address, ISeed<AddressDbM>
{
    [Key]
    public override Guid AddressId { get; set; }

    AddressDbM ISeed<AddressDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}