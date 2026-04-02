
using Microsoft.AspNetCore.Identity;

namespace NexusPatagonia.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;

        public ICollection<Company> Companies { get; set; } = new List<Company>();
    }
}
