using NexusPatagonia.Domain.Common;

namespace NexusPatagonia.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Cuit { get; set; }
        public string MunicipalAccount { get; set; } = "0";
        public virtual ICollection<User> Users { get; set; } = new List<User>();


        public virtual List<Employee> Employees { get; set; }

        public virtual List<MonthlyConcept> MonthlyConcepts { get; set; }

        public virtual List<Uthgra> Uthgras { get; set; }
        public virtual List<DDJJ> DDJJs { get; set; }

        public virtual List<IIBB> IIBBs { get; set; }
        public virtual List<Tish> Tishes { get; set; }

        public virtual List<ProfitabilityReport> ProfitabilityReports { get; set; }

        public virtual List<Sale> Sales { get; set; }

        public virtual List<CashMovement> CashMovements { get; set; }

    }
}
