using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Receipt> _dbSet;
        public ReceiptRepository(ApplicationDbContext context) 
        { 
            _context = context;
            _dbSet = context.Set<Receipt>();
        }

        public async Task AddAsync(Receipt receipt)
        { 
            _dbSet.Add(receipt);
            _context.SaveChanges();
        }

        public async Task<decimal> GetAmountByPeriod(Guid companyId, DateTime period)
        {
            decimal multiplier = decimal.Parse("0.55");
            var amount = await  _dbSet.AsNoTracking().
                Where(x => x.CompanyId == companyId && x.Period == period)
                .SumAsync(x => x.EarningsWithDeductions * multiplier
                + x.Net);
            return amount;
        }
    }
}
