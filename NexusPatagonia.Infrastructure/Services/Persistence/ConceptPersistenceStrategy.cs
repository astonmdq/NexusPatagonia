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
        private readonly ICompanyRepository _companyRepository;
        private readonly IMonthlyConceptRepository _monthlyConceptRepository;
        private readonly IConceptRepository _conceptRepository;
        public ConceptPersistenceStrategy(ApplicationDbContext context, 
            ICompanyRepository companyRepository,
            IMonthlyConceptRepository monthlyConceptRepository,
            IConceptRepository conceptRepository)
        {
            _context = context;
            _companyRepository = companyRepository;
            _monthlyConceptRepository = monthlyConceptRepository;
            _conceptRepository = conceptRepository;
        }

        public bool CanHandle(IExtractedData data) => data is ConceptDto;

        public async Task SaveAsync(IExtractedData data)
        {
            if (data is ConceptDto concept)
            {
                Employee? employee = null;

                var company = await _companyRepository.GetByCuitAsync(concept.Cuit);
                   
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
                    //var existingConcept = await _context.Concepts.AsNoTracking().FirstOrDefaultAsync(c => c.Code == item.Code);
                    var existingConcept = await _conceptRepository.GetByCodeAsync(item.Code);

                    if (existingConcept is null)
                    {
                        existingConcept = new Concept
                        {
                            Code = item.Code,
                            Description = item.Concept
                        };
                        await _conceptRepository.AddAsync(existingConcept);
                    }

                    await _monthlyConceptRepository.AddAsync(new MonthlyConcept
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
            }
        }
    }
}
