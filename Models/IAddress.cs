namespace Models;

public interface IAddress
{
    public Guid AddressId { get; set; }
    public string Street { get; set; }
    public int ZipCode { get; set; }
    public ICity City { get; set; }    
}