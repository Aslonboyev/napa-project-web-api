using Microsoft.EntityFrameworkCore;
using NapaProject.Enums;
using NapaProject.Models;

namespace NapaProject.AppDbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Admin 
        //Password => 12345678
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Role = Role.Admin,
                Password = "$2a$11$1r13m3Pbaonn9R7dhwH3quvYB9xfOlj.XWA31rQNy3EWAldNi0Zdm",
                Salt = "f13471fb-1538-4821-86e8-2e385f63f1b5",
                Username = "admin",
            });
        }
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<ProductAudit> ProductAudits { get; set; } = null!;

        public virtual DbSet<Product> Products { get; set; } = null!;
    }
}
