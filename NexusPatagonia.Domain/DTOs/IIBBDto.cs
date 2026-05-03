namespace NexusPatagonia.Domain.DTOs
{
    public class IIBBDto : IExtractedData
    {
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }
        public string Cuit { get; set; }

        public string BusinessName { get; set; }
    }
}
