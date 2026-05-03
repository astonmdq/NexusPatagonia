using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface ICheckingAccountRepository
    {
        Task AddAsync(CheckingAccount checkingAccount);

        Task<decimal> GetAmountByPeriod(Guid companyId, DateTime period);
    }
}
