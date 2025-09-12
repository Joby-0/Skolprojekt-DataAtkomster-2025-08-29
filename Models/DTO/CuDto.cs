namespace Models.DTO;

public class SightCuDto
{
    public Guid? SightId { get; set; }

    public string SightName { get; set; }
    public string Description { get; set; }

    public Guid AddressId { get; set; }
    public List<Guid> CategoriesId { get; set; }
    public List<Guid> ReviewsId { get; set; }
    public SightCuDto() { }
    public SightCuDto(ISight org)
    {
        SightId = org.SightId;
        SightName = org.SightName;
        Description = org.Description;

        AddressId = org.Address.AddressId;
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