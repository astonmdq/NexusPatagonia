using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class CheckingAccount : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        public decimal Amount { get; set; } 
        public DateTime Period { get; set;  }
    }
}
