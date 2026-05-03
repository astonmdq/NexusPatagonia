using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Interfaces;

namespace NexusPatagonia.Application.Services
{
    public class CashMovementRepository : ICashMovementRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CashMovement> _dbSet;

        public CashMovementRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<CashMovement>();
        }

        public async Task AddAsync(CashMovement entity)
        {
            await _context.CashMovements.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<CashMovementDetailDto> GetByIdAsync(Guid id)
        {
            var result = await _dbSet.FindAsync(id);

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
            var results = await _dbSet.Include(cm => cm.Category)
                                                      .Include(cm => cm.Subcategory)
                                                      .Include(cm => cm.Employee)
                                                      .ToListAsync();
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

        public async Task<decimal> GetAmountsByPeriodAsync(Guid companyId, DateTime period)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(s => s.CompanyId == companyId
                         && s.Date.Month == period.Month
                         && s.Date.Year == period.Year
                         && s.Invoiced
                         && !s.Category.EmployeeRelated 
                         && s.Active)
                .SumAsync(s => s.Expense ? s.Amount : -s.Amount);
        }

        public async Task<decimal> GetSalaryByPeriodAsync(Guid companyId, DateTime period)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(s => s.CompanyId == companyId
                         && s.Date.Month == period.Month
                         && s.Date.Year == period.Year
                         && s.Invoiced
                         && s.Category.EmployeeRelated
                         && s.Active)
                .SumAsync(s => s.Expense? s.Amount:-s.Amount);
        }
    }
}
