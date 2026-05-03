namespace NexusPatagonia.Domain.DTOs
{
    public class TishDto  : IExtractedData
    {
        public DateTime Period { get; set; }
        public Decimal Amount { get; set; }
        public string Company { get; set; }
        public string MunicipalAccount { get; set; }
    }
}
