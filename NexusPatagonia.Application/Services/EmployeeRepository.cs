using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Employee> _dbSet;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Employee>();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.Select(e => new EmployeeDto
            {
                Id = e.Id.ToString(),
                Name = e.Name,
                Cuit = e.Cuit,
                File = e.File
            }).ToListAsync();
            return employees;
        }

        public async Task AddAsync(Employee employee)
        { 
            await _dbSet.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetByFileAsync(int file)
        {
            var employee = await _dbSet.AsNoTracking()
                .Where(x => x.File == file)
                .FirstOrDefaultAsync();
            return employee;
        }
    }
}
