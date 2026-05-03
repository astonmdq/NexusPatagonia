using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;

namespace NexusPatagonia.Infrastructure.Services.Persistence
{
    public class ReceiptPersistenceStrategy : IPersistenceStrategy
    {
        private readonly ApplicationDbContext _context;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IReceiptRepository _receiptRepository;
        public ReceiptPersistenceStrategy(ApplicationDbContext context,
            ICompanyRepository companyRepository,
            IEmployeeRepository employeeRepository,
            IReceiptRepository receiptRepository    )
        {
            _context = context;
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
            _receiptRepository = receiptRepository;
        }

        public bool CanHandle(IExtractedData data) => data is ReceiptDto;

        public async Task SaveAsync(IExtractedData data)
        {
            if (data is ReceiptDto receipt)
            {
                Employee? employee = null;
                var company = await _companyRepository.GetByCuitAsync(receipt.Cuit);

                if (company == null)
                {
                    company = new Company
                    {
                        Cuit = receipt.Cuit,
                        Name = receipt.CompanyName,
                        CreatedAt = DateTime.UtcNow,
                        Active = true
                    };
                    await _companyRepository.AddAsync(company);

                }
                else
                {
                    if (!string.Equals(company.Name, receipt.CompanyName))
                        throw new Exception(($"ALERTA DE SEGURIDAD: El CUIT detectado en el PDF ({receipt.Cuit}) " +
                                $"no coincide con el CUIT registrado para {company.Name} ({company.Cuit}). " +
                                "La operación ha sido cancelada."));
                }

                foreach (var item in receipt.EmployeeReceipts)
                {
                    employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(
                        e => e.Cuit == item.Cuil
                        && e.CompanyId == company.Id);
                    if (employee == null)
                    {
                        employee = new Employee
                        {
                            Cuit = item.Cuil,
                            Name = item.Name,
                            CreatedAt = DateTime.UtcNow,
                            Active = true,
                            Document = item.Cuil.Substring(2, 8),
                            File = item.File,
                            CompanyId = company.Id
                        };
                        await _employeeRepository.AddAsync(employee);
                    }

                    
                    await _receiptRepository.AddAsync(new Receipt
                    {
                        Active = true,
                        ArtEarnings = item.ArtEarnings,
                        ArtHb = item.ArtHb,
                        CompanyId = company.Id,
                        ArtWithholdings = item.ArtWithholdings,
                        CreatedAt = DateTime.UtcNow,
                        EarningsWithDeductions = item.EarningsWithDeductions,
                        EarningsWithoutDeductions = item.EarningsWithoutDeductions,
                        FamilyAllowance = item.FamilyAllowance,
                        EmployeeId = employee.Id,
                        Net = item.Net,
                        Period = receipt.Period,
                        Withholdings = item.Withholdings
                    });
                }
            }
        }
        
    }
}
