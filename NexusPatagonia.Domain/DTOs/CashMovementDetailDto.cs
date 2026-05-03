using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Domain.DTOs
{
    public class CashMovementDetailDto
    {
        public bool Expense { get; set; }
        public DateTime Date { get; set; }
        public bool Invoiced { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public Guid? SubcategoryId { get; set; }
        public string? SubcategoryDescription { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
    }
}
