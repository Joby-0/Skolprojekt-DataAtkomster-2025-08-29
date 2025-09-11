namespace Models.DTO;

public class SightDto
{
    public Guid? SightId { get; set; }

    public string SightName { get; set; }
    public string Description { get; set; }

    public Guid Address { get; set; }
    public List<Guid> CategoriesId { get; set; }
    public List<Guid> ReviewsId { get; set; }
}
public class UserDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public List<Guid> ReviewsId { get; set; }
}