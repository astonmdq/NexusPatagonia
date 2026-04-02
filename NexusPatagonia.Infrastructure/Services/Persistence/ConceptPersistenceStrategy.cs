using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Infrastructure.Services.Persistence
{
    public class ConceptPersistenceStrategy : IPersistenceStrategy
    {
        private readonly ApplicationDbContext _context;
        public ConceptPersistenceStrategy(ApplicationDbContext context) => _context = context;

        public bool CanHandle(IExtractedData data) => data is ConceptDto;

        public async Task SaveAsync(IExtractedData data)
        {
            if (data is ConceptDto concept)
            {
                Employee? employee = null;
                var company = await _context.Companies.AsNoTracking().FirstOrDefaultAsync(e => e.Cuit == concept.Cuit);
                if (company == null)
                {
                    company = new Company
                    {
                        Cuit = concept.Cuit,
                        Name = concept.CompanyName,
                        CreatedAt = DateTime.UtcNow,
                        Active = true
                    };
                    _context.Add(company);
                    await _context.SaveChangesAsync();
                }
                
                foreach (var item in concept.ConceptsDetails)
                { 
                    var existingConcept = await _context.Concepts.AsNoTracking().FirstOrDefaultAsync(c => c.Code == item.Code);

                    if (existingConcept is null)
                    {
                        existingConcept = new Concept
                        {
                            Code = item.Code,
                            Description = item.Concept
                        };
                        _context.Add(existingConcept);
                        await _context.SaveChangesAsync();
                    }

                    _context.Add(new MonthlyConcept
                    {
                        CompanyId = company.Id,
                        Active = true,
                        ConceptId = existingConcept.Id,
                        NonTaxable = item.NotTaxed,
                        Net = item.Net,
                        Period = concept.Period,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
