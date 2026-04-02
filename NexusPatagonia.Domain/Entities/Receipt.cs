using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class Receipt : BaseEntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal EarningsWithDeductions { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal EarningsWithoutDeductions { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FamilyAllowance { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Withholdings { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ArtEarnings { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ArtHb { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ArtWithholdings { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Net { get; set; }
        public DateTime Period { get; set; }

        public Guid EmployeeId { get;set;}
        public virtual Employee Employee { get; set; }

        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

    }
}
