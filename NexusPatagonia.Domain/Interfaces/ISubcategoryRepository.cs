namespace NexusPatagonia.Domain.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<bool> ExistsAsync(Guid id);
    }
}
