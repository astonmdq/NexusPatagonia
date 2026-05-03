namespace NexusPatagonia.Domain.DTOs
{
    public class ProfitabilityPdfReportDto
    {
        public string CompanyName { get; set; }
        public string Cuit { get; set; }
        public DateTime Period { get; set; }
        public decimal ResultA { get; set; }
        public decimal ResultB { get; set; }
        public decimal MonthlyConcepts { get; set; }
        public decimal CashMovements { get; set; }
        public decimal FeeFCA { get; set;  }
        public decimal ProfitabilityPercent { get; set; }
        public decimal FeeFCB { get; set; }
        public decimal PreviousFeeFCA { get; set; }
        public decimal PreviousFeeFCB { get; set; }
        public decimal Receipts { get; set; }
        public decimal CheckingAccounts { get; set; }
        public decimal SalaryMovements { get; set; }
        public decimal IIBB { get; set; }
        public decimal Tish { get; set; }

        public decimal NetResult { get; set; }
        public decimal GrossResultMonthly { get; set; }

        public decimal IncomeTax { get; set; }
        public decimal TotalSale { get; set;  }

    }
}
