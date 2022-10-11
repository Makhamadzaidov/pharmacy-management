using Microsoft.EntityFrameworkCore;
using PharmacyAppExam.WebApi.Models;

namespace PharmacyAppExam.WebApi.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<Drug> Drugs { get; set; } = null!;

        public virtual DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasIndex(user => user.Email)
                    .IsUnique();

            modelBuilder.Entity<User>()
                        .HasIndex(user => user.PhoneNumber)
                        .IsUnique();

            modelBuilder.Entity<Drug>()
                        .HasIndex(drug => drug.Name)
                        .IsUnique();
        }
    }
}
