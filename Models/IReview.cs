namespace Models;

public interface IReview
{
    public Guid ReviewId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Created_at { get; set; }
    public Sight Sight { get; set; }
    public User User { get; set; }
    
}