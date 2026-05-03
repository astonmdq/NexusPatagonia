using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(Guid id);
        Task<Company> GetByCuitAsync(string cuit);

        Task AddAsync(Company company);
    }
}
