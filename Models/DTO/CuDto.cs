using Joby.Utilities.SeedGenerator;

namespace Models.DTO;

public class SightCuDto
{
    public Guid? SightId { get; set; }

    public string SightName { get; set; }
    public string Description { get; set; }

    public Guid? AddressId { get; set; }
    public List<Guid> CategoriesId { get; set; }
    public List<Guid> ReviewsId { get; set; }
    public SightCuDto() { }
    public SightCuDto(ISight org)
    {
        SightId = org.SightId;
        SightName = org.SightName;
        Description = org.Description;

        AddressId = org?.Address.AddressId;
        CategoriesId = org.Categories?.Select(i => i.CategoryId).ToList();
        ReviewsId = org.Reviews?.Select(i => i.ReviewId).ToList();
    }
}
public class UserCuDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public List<Guid> ReviewsId { get; set; }
}

public class ReviewCuDto
{
    public Guid? ReviewId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime Created_at { get; set; }
    public Guid SightId { get; set; }
    public Guid UserId { get; set; }

    public ReviewCuDto() { }
    public ReviewCuDto(IReview org)
    {
        ReviewId = org.ReviewId;
        Rating = org.Rating;
        Comment = org.Comment;
        Created_at = org.Created_at;

        SightId = org.Sight.SightId;
        UserId = org.User.UserId;

    }
}

