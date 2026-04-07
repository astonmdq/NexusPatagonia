namespace NexusPatagonia.Domain.DTOs
{
    public class CashMovementSaveDto
    {
        public bool Expense { get; set; }
        public DateTime Date { get; set; }
        public bool Invoiced { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
