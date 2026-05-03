using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale entity);
        Task<Sale?> GetByPeriodAsync(Guid companyId, DateTime period);
    }
}
