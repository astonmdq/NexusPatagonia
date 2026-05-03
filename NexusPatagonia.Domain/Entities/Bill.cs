using NexusPatagonia.Domain.Common;
using NexusPatagonia.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public DateTime Date { get; set; }
        public BillType Type { get; set; }
        public CategoryType Category { get; set; }
        public string Number { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Net { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NonTaxableItems { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExemptOperations { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscriminatedVat { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscriminatedIncreaseItems { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxCredit { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal WithholdingsPerceptionsPaymentOnAccount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOperationProvince { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal VatRegister { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }



    }
}
