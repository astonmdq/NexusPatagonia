using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class Sale : BaseEntity
    {
        // facturas venta
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalesInvoice{ get; set; }
        // facturas interna venta
        [Column(TypeName = "decimal(18,2)")]
        public decimal InternalSalesInvoice{ get; set; }
        // nota de crédito
        [Column(TypeName = "decimal(18,2)")]
        public decimal CreditNote{ get; set; }
        // nota de débito
        [Column(TypeName = "decimal(18,2)")]
        public decimal DebitNote{ get; set; }
        // nota de crédito interna venta
        [Column(TypeName = "decimal(18,2)")]
        public decimal InternalSalesCreditNote{ get; set; }
        // nota de débito interna venta
        [Column(TypeName = "decimal(18,2)")]
        public decimal InternalSalesDebitNote{ get; set; }
        public DateTime Period { get; set; }
        public Guid CompanyId { get;set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
    }
}
