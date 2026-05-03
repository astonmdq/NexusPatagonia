namespace NexusPatagonia.Domain.DTOs
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool EmployeeRelated { get; set; }
        public List<SubcategoryDto>? Subcategories { get; set; }
    }
}
