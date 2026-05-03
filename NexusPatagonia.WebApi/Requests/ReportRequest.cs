namespace NexusPatagonia.Requests
{
    public class ReportRequest
    {
        public Guid CompanyId { get; set;  }
        public string Month { get; set; }
        public int Year { get; set; }
        public decimal IIBB { get; set; }
    }
}
