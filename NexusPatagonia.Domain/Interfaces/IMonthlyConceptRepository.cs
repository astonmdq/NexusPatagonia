using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IMonthlyConceptRepository
    {
        Task<decimal> GetAmountByPeriod(Guid companyId, DateTime period);

        Task AddAsync(MonthlyConcept monthlyConcept);
    }
}
