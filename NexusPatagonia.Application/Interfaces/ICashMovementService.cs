using NexusPatagonia.Domain.DTOs;

namespace NexusPatagonia.Application.Interfaces
{
    public interface ICashMovementService
    {
        public Task<CashMovementDetailDto?> GetByIdAsync(Guid Id);
        public Task<List<CashMovementDetailDto>> GetAll();
        public Task<CashMovementDto> RegisterCashMovement(CashMovementSaveDto cashMovement);
    }
}
