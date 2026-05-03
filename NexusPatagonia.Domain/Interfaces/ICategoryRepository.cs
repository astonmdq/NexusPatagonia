using NexusPatagonia.Domain.DTOs;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryDto>> GetAllAsync();

        public Task<bool> ExistsAsync(Guid id);
    }
}
