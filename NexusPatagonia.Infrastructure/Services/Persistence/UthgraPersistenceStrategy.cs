using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Infrastructure.Services.Persistence
{
    public class UthgraPersistenceStrategy : IPersistenceStrategy
    {
        private readonly ApplicationDbContext _context;
        public UthgraPersistenceStrategy(ApplicationDbContext context) => _context = context;

        public bool CanHandle(IExtractedData data) => data is UthgraDto;
        

        public async Task SaveAsync(IExtractedData data)
        {
            if (data is UthgraDto uthgra)
            {
                var company = await _context.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Cuit == uthgra.Cuit);

                if (company == null)
                {
                    company = new Company
                    {
                        Cuit = uthgra.Cuit,
                        Name = uthgra.CompanyName,
                        CreatedAt = DateTime.UtcNow,
                        Active = true
                    };
                    _context.Add(company);
                    await _context.SaveChangesAsync();
                }

                foreach (var item in uthgra.UthgraDetails)
                {
                    var existingConcept = await _context.UthgraConcepts.AsNoTracking().FirstOrDefaultAsync(
                        c => c.Description == item.Description);

                    if (existingConcept == null)
                    {
                        existingConcept = new UthgraConcept
                        {
                            Description = item.Description,
                            CreatedAt = DateTime.UtcNow,
                            Active = true
                        };
                        _context.Add(existingConcept);
                        await _context.SaveChangesAsync();
                    }

                    _context.Uthgras.Add(new Uthgra
                    {
                        CompanyId = company.Id,
                        UthgraConceptId = existingConcept.Id,
                        Period = (DateTime)uthgra.Period,
                        Amount = item.Amount,
                        Active = true
                    });
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
