using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Domain.DTOs
{
    public class ReceiptDto : IExtractedData
    { 
        public string Cuit{ get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime Period { get; set; }
        public List<EmployeeReceiptDto> EmployeeReceipts { get; set; }

    }
    public class EmployeeReceiptDto
    {
        public string File { get; set; }
        public string Name { get; set; }
        public string Cuil { get; set; }
        public decimal EarningsWithDeductions { get; set; }
        public decimal EarningsWithoutDeductions { get; set; }
        public decimal FamilyAllowance { get; set; }
        public decimal Withholdings { get; set; }
        public decimal ArtEarnings { get; set; }
        public decimal ArtHb { get; set; }
        public decimal ArtWithholdings { get; set; }
        public decimal Net { get; set; }
        
    }

}
