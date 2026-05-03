using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface ITishRepository
    {
        Task AddAsync(Tish tish);

        Task<decimal> GetByPeriod(Guid companyId,DateTime period);
    }
}
