using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class CheckingAccountRepository : ICheckingAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<CheckingAccount> _dbSet;
        public CheckingAccountRepository(ApplicationDbContext context) 
        { 
            _context= context;
            _dbSet = _context.Set<CheckingAccount>();
        }
        public async Task AddAsync(CheckingAccount checkingAccount)
        { 
            await _dbSet.AddAsync(checkingAccount);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetAmountByPeriod(Guid companyId, DateTime period)
        { 
            var amount = await _dbSet.AsNoTracking()
                        .Where(x => x.Employee.CompanyId == companyId
                            && x.Period == period)
                        .SumAsync(x => x.Amount);
            return amount;
        }
    }
}
