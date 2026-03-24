using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class Receipt : BaseEntity
    {
        public decimal EarningsWithDeductions { get; set; }
        public decimal EarningsWithoutDeductions { get; set; }
        public decimal FamilyAllowance { get; set; }
        public decimal Withholdings { get; set; }
        public decimal ArtEarnings { get; set; }
        public decimal ArtHb { get; set; }
        public decimal ArtWithholdings { get; set; }
        public decimal Net { get; set; }
        public DateTime Period { get; set; }

        public Guid EmployeeId { get;set;}
        public virtual Employee Employee { get; set; }

    }
}
