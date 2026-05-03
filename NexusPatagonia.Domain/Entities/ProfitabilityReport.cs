using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class ProfitabilityReport : BaseEntity
    {
        public DateTime Period { get; set; }
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalesRevenue { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purhcases { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnvoucheredExpenses { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyServiceFeeA { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal MonthlyServiceFeeB { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal SalariesAndPayrollTaxes { get; set;  }
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrossReceiptsTaxExpense { get; set; }

    }
}
