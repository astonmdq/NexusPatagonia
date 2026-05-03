namespace NexusPatagonia.Domain.DTOs
{
    public class UthgraDto : IExtractedData
    {
        public DateTime? Period { get; set; }
        public string Cuit { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public List<UthgraDetailDto> UthgraDetails { get; set; }
    }

    public class UthgraDetailDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
