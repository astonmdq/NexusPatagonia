using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class CashMovement : BaseEntity
    {
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public bool Expense { get; set; }
        public bool Invoiced { get; set; }
        public string Details { get; set;  }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public Guid? SubcategoryId { get; set; }
        [ForeignKey("SubcategordyId")]
        public virtual Subcategory Subcategory { get; set; }    

        public Guid? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
