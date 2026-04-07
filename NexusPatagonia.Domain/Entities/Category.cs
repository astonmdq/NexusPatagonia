using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }

        public virtual List<Subcategory> Subcategories { get; set; }

        public virtual List<CashMovement> CashMovements { get; set; }
    }
}
