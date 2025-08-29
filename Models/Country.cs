
namespace Models;

public class Country : ICountry
{
    public Guid CountryId { get; set; }
    public string CountryName { get; set; }
}