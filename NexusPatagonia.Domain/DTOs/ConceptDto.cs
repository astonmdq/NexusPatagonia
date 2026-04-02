using NexusPatagonia.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.DTOs
{
    public class ConceptDto : IExtractedData
    {
        public DateTime Period { get; set; }
        public string Cuit { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public List<ConceptDetailDto> ConceptsDetails { get; set; }
    }
    public class ConceptDetailDto
    {
        public string Code { get; set; }
        public string Concept { get; set; }
        public decimal Net { get; set; }
        public decimal NotTaxed { get; set; }
    }
}
