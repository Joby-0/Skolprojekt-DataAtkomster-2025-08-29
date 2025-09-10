using Models;
using Models.DTO;

namespace Services;

public interface ISightService
{
    public Task SeedAsync();
    public Task<ResponsePageDto<ISight>> ReadSightsAsync();
}

