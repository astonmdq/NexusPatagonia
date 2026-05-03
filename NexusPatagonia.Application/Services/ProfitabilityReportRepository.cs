using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class ProfitabilityReportRepository : IProfitabilityReportRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProfitabilityReport> _dbSet;

        public ProfitabilityReportRepository(ApplicationDbContext context) 
        { 
            _context = context;
            _dbSet = _context.Set<ProfitabilityReport>();
        }

        public async Task<Tuple<decimal, decimal>> GetPreviousFeeFc(Guid companyId, DateTime currentPeriod)
        {
            var previousFeeFC = await _dbSet.AsNoTracking()
                .Where(x => x.CompanyId == companyId && x.Period == currentPeriod.AddMonths(-1))
                .Select(x => new Tuple<decimal, decimal>(x.MonthlyServiceFeeA, x.MonthlyServiceFeeB))
                .FirstOrDefaultAsync();

            return previousFeeFC ?? new Tuple<decimal, decimal>(previousFeeFC.Item1, previousFeeFC.Item2);
        }
    }
}
