using Models;
using Models.DTO;

namespace Services;

public interface ISightService
{
    public Task<ResponseItemDto<ISight>> ReadSightAsync(Guid id, bool flat);
    public Task<ResponsePageDto<ISight>> ReadSightsAsync(bool seeded, bool flat, string filter, int pageNumber, int pageSize);

    public Task<ResponseItemDto<ISight>> DeleteSightAsync(Guid id);
}

