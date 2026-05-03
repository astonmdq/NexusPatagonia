using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }
    }
}
