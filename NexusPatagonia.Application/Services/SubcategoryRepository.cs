using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Subcategory> _dbSet;

        public SubcategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Subcategory>();
        }   
        public async Task<bool> ExistsAsync(Guid id)
        { 
            var exists = await _dbSet.FindAsync(id);
            return exists != null;
        }
    }
}
