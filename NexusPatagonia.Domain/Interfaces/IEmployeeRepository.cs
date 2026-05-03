using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task AddAsync(Employee employee);

        Task<Employee> GetByFileAsync(int file);
    }
}
