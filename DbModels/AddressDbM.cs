using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Joby.Utilities.SeedGenerator;
using Models;

namespace DbModels;

public class AddressDbM : Address, ISeed<AddressDbM>
{
    [Key]
    public override Guid AddressId { get; set; }
    [Required]
    public override string Street { get; set; }
    [Required]
    public override int ZipCode { get; set; }

    [NotMapped]
    public override ICity City { get; set; }

    AddressDbM ISeed<AddressDbM>.Seed(SeedGenerator seedGenerator)
    {
        base.Seed(seedGenerator);
        return this;
    }
}