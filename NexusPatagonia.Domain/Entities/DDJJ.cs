using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class DDJJ : BaseEntity
    {
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        public Guid DDJJConceptId { get; set; }
        [ForeignKey("DDJJConceptId")]
        public virtual DDJJConcept DDJJConcept { get; set; }    
        public DateTime Period { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}
