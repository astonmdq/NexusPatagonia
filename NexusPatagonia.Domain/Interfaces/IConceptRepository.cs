using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IConceptRepository
    {
        Task<Concept?> GetByCodeAsync(string code);
        Task AddAsync(Concept concept);
    }
}
