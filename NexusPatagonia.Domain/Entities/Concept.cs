using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class Concept : BaseEntity
    {
        
        public string Description { get; set; }
        
        public string Code { get; set; }
        public List<MonthlyConcept> MonthlyConcepts { get; set; }
        public bool ProfitReport { get; set; } = true;
    }
}
