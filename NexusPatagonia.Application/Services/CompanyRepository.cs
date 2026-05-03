using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Company> _dbSet;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Company>();
        }

        public async Task<Company?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<Company>> GetAllActiveAsync()
        {
            return await _dbSet.Where(c => c.Active).AsNoTracking().ToListAsync();
        }

        public async Task<Company?> GetByCuitAsync(string cuit)
        {
            return await _dbSet.Where(x => x.Cuit == cuit).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task AddAsync(Company company)
        {
            await _dbSet.AddAsync(company);
            await _context.SaveChangesAsync();
        }
    }
}
