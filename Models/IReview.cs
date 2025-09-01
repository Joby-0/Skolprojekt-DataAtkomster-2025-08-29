namespace Models;

public interface IReview
{
    public Guid ReviewId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Created_at { get; set; }
    public ISight Sight { get; set; }
    public IUser User { get; set; }
    
}