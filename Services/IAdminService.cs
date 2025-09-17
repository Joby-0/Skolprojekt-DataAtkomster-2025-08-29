using Models;
using Models.DTO;

namespace Services;

public interface IAdminService
{
    public Task SeedAsync(string nrOfItems);
    public Task<ResponseItemDto<GstUsrInfoAllDto>> DbInfo();
    public Task<GstUsrInfoDbDto> RemoveSeedAsync();

}

