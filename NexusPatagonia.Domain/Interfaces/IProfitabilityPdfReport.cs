using NexusPatagonia.Domain.DTOs;

namespace NexusPatagonia.Domain.Interfaces
{
    public interface IProfitabilityPdfReport
    {
        byte[] GenerateReport(ProfitabilityPdfReportDto data);
    }
}
