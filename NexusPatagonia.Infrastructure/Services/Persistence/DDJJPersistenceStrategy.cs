using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Infrastructure.Services.Persistence
{
    public class DDJJPersistenceStrategy : IPersistenceStrategy
    {
        private readonly ApplicationDbContext _context; // Tu DbContext de Entity Framework

        public DDJJPersistenceStrategy(ApplicationDbContext context) => _context = context;

        public bool CanHandle(IExtractedData data) => data is DDJJDto;

        public async Task SaveAsync(IExtractedData data)
        {
            var ddjj = data as DDJJDto;
            if (ddjj == null) return;

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Cuit == ddjj.Cuit);

            if (company == null)
            {
                company = new Company()
                {
                    Cuit = ddjj.Cuit,
                    Name = ddjj.BusinessName
                };
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
            }

            DDJJConcept? ddjjConcept;
            foreach (var item in ddjj.Details)
            { 
                ddjjConcept = await _context.DDJJConcepts.FirstOrDefaultAsync(c => c.Code == item.Code);
                if (ddjjConcept == null)
                { 
                    ddjjConcept = new DDJJConcept()
                    {
                        Code = item.Code,
                        Description = item.Description
                    };
                    _context.Add(ddjjConcept);
                    await _context.SaveChangesAsync();
                }
                _context.Add(new DDJJ()
                {
                    CompanyId = company.Id,
                    DDJJConceptId = ddjjConcept.Id,
                    Amount = item.Amount,
                    Period = ddjj.Period ?? new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddMonths(-1)
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}
