using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IReceiptRepository
    {
        Task AddAsync(Receipt receipt);

        Task<decimal> GetAmountByPeriod(Guid companyId, DateTime period);
    }
}
