using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class ConceptRepository : IConceptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Concept> _dbSet;
        public ConceptRepository(ApplicationDbContext context) 
        {
            _context = context;
            _dbSet = _context.Set<Concept>();
        }

        public async Task<Concept?> GetByCodeAsync(string code)
        { 
            var concept = await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Code == code);
            return concept;
        }

        public async Task AddAsync(Concept concept)
        { 
            await _dbSet.AddAsync(concept);
            await _context.SaveChangesAsync();
        }

    }
}
