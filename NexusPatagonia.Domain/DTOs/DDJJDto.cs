namespace NexusPatagonia.Domain.DTOs
{
    public class DDJJDto : IExtractedData
    {
        public string DocumentType => "ArcaF931";
        public string BusinessName;
        public string Cuit { get; set; }
        public DateTime? Period { get; set; }
        public List<F931DetailDto> Details { get; set; } = new List<F931DetailDto>();
    }

    public class F931DetailDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
