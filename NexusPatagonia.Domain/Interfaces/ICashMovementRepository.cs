using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface ICashMovementRepository
    {
        public Task AddAsync(CashMovement entity);
        public Task<CashMovementDetailDto> GetByIdAsync(Guid id);

        public Task<List<CashMovementDetailDto>> GetAllAsync();

        public Task<decimal> GetAmountsByPeriodAsync(Guid companyId, DateTime period);

        public Task<decimal> GetSalaryByPeriodAsync(Guid companyId, DateTime period);
    }
}
