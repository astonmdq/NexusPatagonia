using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class DDJJConcept : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual List<DDJJ> DDJJs { get; set; }
    }
}
