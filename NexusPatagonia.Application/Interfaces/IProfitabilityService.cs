using NexusPatagonia.Domain.DTOs;

namespace NexusPatagonia.Application.Interfaces
{
    public interface IProfitabilityService
    {
        Task<byte[]> GeneratePdf(ProfitabilityReportDto report);
    }
}
