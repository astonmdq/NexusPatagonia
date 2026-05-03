using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class MonthlyConceptRepository : IMonthlyConceptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<MonthlyConcept> _dbSet;
        public MonthlyConceptRepository(ApplicationDbContext context) 
        { 
            _context = context;
            _dbSet = _context.Set<MonthlyConcept>();
        }

        public async Task<decimal> GetAmountByPeriod(Guid companyId, DateTime period)
        {
            var amount = await _dbSet.AsNoTracking()
                    .Where(x => x.CompanyId == companyId 
                    && x.Period == period)
                    .SumAsync(x => x.Net + x.NonTaxable);
            return amount;
        }

        public async Task AddAsync(MonthlyConcept monthlyConcept)
        { 
            await _dbSet.AddAsync(monthlyConcept);
            await _context.SaveChangesAsync();
        }
            
    }
}
