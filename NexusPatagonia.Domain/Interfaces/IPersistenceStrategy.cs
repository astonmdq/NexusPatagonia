using NexusPatagonia.Domain.DTOs;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IPersistenceStrategy
    {
        bool CanHandle(IExtractedData data);
        Task SaveAsync(IExtractedData data);
    }
}
