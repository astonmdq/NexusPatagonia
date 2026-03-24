using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Definición de Tablas
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");        
        }
    }
}
