using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Application.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _dbSet;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Category>();
        }
        public async Task<List<CategoryDto>> GetAllAsync()
        { 
            var results = await _context.Categories
                .Include(c => c.Subcategories)
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Description = c.Description,
                    EmployeeRelated = c.EmployeeRelated,
                    Subcategories = c.Subcategories.Select(s => new SubcategoryDto
                    {
                        Id = s.Id,
                        Description = s.Description
                    }).ToList()
                }).ToListAsync();

            return results;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            var exists = await _context.Categories.FindAsync(id);
            return exists != null;
        }
    }
}
