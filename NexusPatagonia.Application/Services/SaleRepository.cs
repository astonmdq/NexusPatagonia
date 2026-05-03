using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Sale> _dbSet;

        public SaleRepository(ApplicationDbContext context) { 
            _context = context; 
            _dbSet = _context.Set<Sale>();
        }
        public async Task AddAsync(Sale entity) {
            await _context.Sales.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Sale?> GetByPeriodAsync(Guid companyId, DateTime period)
        {
            return await _context.Sales
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CompanyId == companyId && x.Period == period);
        }
    }
}
