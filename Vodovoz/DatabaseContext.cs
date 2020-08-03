using Microsoft.EntityFrameworkCore;

using Vodovoz.Model;

namespace Vodovoz
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=vodovoz;Username=despair;Password=password;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasKey(u => u.DepartmentId);
            modelBuilder.Entity<Employee>().HasKey(u => u.EmployeeId);
            modelBuilder.Entity<Order>().HasKey(u => u.OrderId);
        }
    }
}