namespace Models;

public interface ISight
{
    public Guid SightId { get; set; }

    public string SightName { get; set; }
    public string Description { get; set; }

    public IAddress Address { get; set; }

    public List<ICategory> Categories { get; set; }
    public List<IReview> Reviews { get; set; }

    
}