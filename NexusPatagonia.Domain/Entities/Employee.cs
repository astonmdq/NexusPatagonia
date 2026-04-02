using NexusPatagonia.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusPatagonia.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Cuit { get; set; }
        public string Document { get; set; }
        public string File { get; set; }
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]

        public virtual Company Company { get; set; }

        public virtual List<Receipt> Receipts { get; set; }
    }
}
