using NexusPatagonia.Domain.Common;
using NexusPatagonia.Domain.Enums;

namespace NexusPatagonia.Domain.Entities
{
    public class Bill : BaseEntity
    {
        public DateTime Date { get; set; }
        public BillType Type { get; set; }
        public CategoryType Category { get; set; }
        public string Number { get; set; }
        
        public decimal Net { get; set; }
        public decimal NonTaxableItems { get; set; }
        public decimal ExemptOperations { get; set; }
        public decimal DiscriminatedVat { get; set; }
        public decimal DiscriminatedIncreaseItems { get; set; }
        public decimal TaxCredit { get; set; }
        public decimal WithholdingsPerceptionsPaymentOnAccount { get; set; }
        public decimal TotalOperationProvince { get; set; }

        public decimal VatRegister { get; set; }
        public Guid SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }



    }
}
