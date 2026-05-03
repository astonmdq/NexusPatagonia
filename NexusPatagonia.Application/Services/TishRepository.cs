using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class TishRepository : ITishRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Tish> _dbSet;

        public TishRepository(ApplicationDbContext context)
        { 
            _context = context;
            _dbSet = _context.Set<Tish>();
        }
        public async Task AddAsync(Tish tish)
        { 
            await  _dbSet.AddAsync(tish);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetByPeriod(Guid companyId, DateTime period)
        {
            var amount = await _dbSet.AsNoTracking()
                .Where(x => x.CompanyId == companyId && x.Period == period)
                .Select(x => x.Amount)
                .FirstOrDefaultAsync();

            return amount;
        }
    }
}
