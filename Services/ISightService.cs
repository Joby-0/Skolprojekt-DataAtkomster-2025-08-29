using Models;
using Models.DTO;

namespace Services;

public interface ISightService
{
    public Task SeedAsync(string nrOfItems);
    public Task<ResponsePageDto<ISight>> ReadSightsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);
}

