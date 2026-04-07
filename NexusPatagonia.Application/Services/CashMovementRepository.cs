using NexusPatagonia.Application.Interfaces;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace NexusPatagonia.Application.Services
{
    public class CashMovementRepository : ICashMovementRepository
    {
        private readonly ApplicationDbContext _context;

        public CashMovementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CashMovement entity)
        {
            await _context.CashMovements.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<CashMovementDetailDto> GetByIdAsync(Guid id)
        {
            var result = await _context.CashMovements.FindAsync(id);

            return new CashMovementDetailDto()
            {
                Invoiced = result.Invoiced,
                Amount = result.Amount,
                CategoryId = result.CategoryId,
                CategoryDescription = result.Category.Description,
                SubcategoryId = result.SubcategoryId,
                SubcategoryDescription = result.Subcategory?.Description,
                Date = result.Date,
                Description = result.Details,
                EmployeeId = result.EmployeeId,
                EmployeeName = result.Employee?.Name
            };
        }

        public async Task<List<CashMovementDetailDto>> GetAllAsync()
        {
            var results = await _context.CashMovements.ToListAsync();
            return results.Select(result => new CashMovementDetailDto()
            {
                Invoiced = result.Invoiced,
                Amount = result.Amount,
                CategoryId = result.CategoryId,
                CategoryDescription = result.Category.Description,
                SubcategoryId = result.SubcategoryId,
                SubcategoryDescription = result.Subcategory?.Description,
                Date = result.Date,
                Description = result.Details,
                EmployeeId = result.EmployeeId,
                EmployeeName = result.Employee?.Name
            }).ToList();
        }
    }
}
