using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Common;
using NexusPatagonia.Domain.Entities;

namespace NexusPatagonia.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Definición de Tablas
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Concept> Concepts { get; set; }
        public DbSet<MonthlyConcept> MontlysConcepts { get; set; }
        public DbSet<Uthgra> Uthgras { get; set; }
        public DbSet<UthgraConcept> UthgraConcepts { get; set; }

        public DbSet<DDJJ> DDJJs { get; set; }
        public DbSet<DDJJConcept> DDJJConcepts { get; set; }

        public DbSet<CashMovement> CashMovements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }   

        public DbSet<IIBB> IIBBs { get; set; }

        public DbSet<Tish> Tishes { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<ProfitabilityReport> ProfitabilityReports { get; set; }    
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var adminId = Guid.Parse("B22C1935-7AAD-4D72-88E0-9E11C28F2A14");

            var adminUser = new User
            {
                Id = adminId,
                Name = "admin@nexuspatagonia.com",
                NormalizedUserName = "ADMIN@NEXUSPATAGONIA.COM",
                Email = "admin@nexuspatagonia.com",
                NormalizedEmail = "ADMIN@NEXUSPATAGONIA.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin@nexuspatagonia.com"
            };

            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Nexus123!", HashType.SHA256);

            modelBuilder.Entity<User>().HasData(adminUser);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)          // El empleado tiene una compañía
                .WithMany(c => c.Employees)      // La compañía tiene muchos empleados
                .HasForeignKey(e => e.CompanyId) // Especificamos la FK
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Companies)
                .WithMany(c => c.Users)
                .UsingEntity(j => j.ToTable("UserCompanies"));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasKey("Id");
                    modelBuilder.Entity(entityType.ClrType).Property("Id").ValueGeneratedOnAdd();
                        
                }
            }
        }

        
    }
}
