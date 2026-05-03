using NexusPatagonia.Domain.DTOs;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IPersistenceCoordinator
    {
        Task SaveAsync(IExtractedData data);
    }
}
