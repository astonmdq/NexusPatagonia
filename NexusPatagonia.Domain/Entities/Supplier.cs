using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string CompanyName { get; set; }
        public string Cuit { get; set; }

        public string Direction { get; set;}
        public string PhoneNumber { get; set;}
        public string ContactName { get; set;}
        public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
    }
}
