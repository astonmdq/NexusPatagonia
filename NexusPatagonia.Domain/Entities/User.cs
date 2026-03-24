
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;

        public virtual List<Company> Companies { get; set; }
    }
}
