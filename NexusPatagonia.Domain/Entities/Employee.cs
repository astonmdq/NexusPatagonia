using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Cuit { get; set; }
        public string Document { get; set; }
        public string File { get; set; }

        public virtual List<Company> Companies { get; set; }
    }
}
