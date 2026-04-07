using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Application.Interfaces
{
    public interface ICashMovementRepository
    {
        public Task AddAsync(CashMovement entity);
        public Task<CashMovementDetailDto> GetByIdAsync(Guid id);

        public Task<List<CashMovementDetailDto>> GetAllAsync();
    }
}
