namespace Services;

public interface IUserService
{
    public Task SeedAsync(string nrOfItems);
}