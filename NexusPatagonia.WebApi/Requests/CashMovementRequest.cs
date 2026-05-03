using System.ComponentModel.DataAnnotations;

namespace NexusPatagonia.Requests
{
    public class CashMovementRequest
    {
        [Required]
        public bool Expense { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool Invoiced { get; set; }
        public string Description { get; set; }
        public decimal Amount { get;set;  }
        [Required]
        public Guid CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid? EmployeeId { get; set; }

        
    }
}
