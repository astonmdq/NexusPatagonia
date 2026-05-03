using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Infrastructure.Services.Persistence
{
    public class TishPersistenceStrategy : IPersistenceStrategy
    {
        private readonly ApplicationDbContext _context;
        public TishPersistenceStrategy(ApplicationDbContext context)
        { 
            _context = context;
        }

        public bool CanHandle(IExtractedData data) => data is TishDto;

        public async Task SaveAsync(IExtractedData data)
        { 
            var tish = data as TishDto;
            if (tish == null) return;

            var company = await _context.Companies.FirstOrDefaultAsync( c => c.MunicipalAccount == tish.MunicipalAccount );

            if (company == null)
            {
                company = new Company()
                {
                    Cuit = string.Empty,
                    Name = tish.Company,
                    MunicipalAccount = tish.MunicipalAccount
                };
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
            }

            _context.Add(new Tish()
            {
                Amount = tish.Amount,
                Period = tish.Period,
                CompanyId = company.Id
            });
            await _context.SaveChangesAsync();
        }
    }
}
