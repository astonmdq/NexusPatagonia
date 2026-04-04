using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Cuit { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public virtual List<Employee> Employees { get; set; }

        public virtual List<MonthlyConcept> MonthlyConcepts { get; set; }

        public virtual List<Uthgra> Uthgras { get; set; }
        public virtual List<DDJJ> DDJJs { get; set; }

    }
}
