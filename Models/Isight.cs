namespace Models;

public interface ISight
{
    public Guid SightId { get; set; }

    public string SightName { get; set; }
    public string Description { get; set; }

    public Address Address { get; set; }

    //man kankse kan göra så här
    
    public List<Category> Categories { get; set; }

    
}