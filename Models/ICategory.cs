namespace Models;

public interface ICategory
{
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    //man kankse kan göra så här
    public ISight Sight { get; set; }
}