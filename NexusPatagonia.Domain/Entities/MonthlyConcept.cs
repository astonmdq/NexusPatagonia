using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class MonthlyConcept : BaseEntity
    {
        public DateTime Period { get; set; }
        public Guid ConceptId { get; set; }
        [ForeignKey("ConceptId")]
        public Concept Concept { get; set; }

        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Net { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal NonTaxable { get; set; }
    }
}
