namespace NexusPatagonia.Domain.DTOs
{
    public class ProfitabilityReportDto
    {
        public DateTime Period { get; set; }
        public Guid CompanyId { get; set; }
        public Decimal IIBB { get; set; }
    }
}
