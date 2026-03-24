using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Cuit { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
