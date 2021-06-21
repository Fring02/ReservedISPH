using ISPH.Domain.Core.Configuration;
using ISPH.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ISPH.Infrastructure.Data.Contexts
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<FeaturedAdvertisement> Favourites { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().
                HasMany(c => c.Employers).
                WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId).
                OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>().Property(c => c.Id).ValueGeneratedOnAdd();
            
            modelBuilder.Entity<Position>().
                HasMany(p => p.Advertisements).
                WithOne(a => a.Position).
                HasForeignKey(a => a.PositionId).
                OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Position>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Student>().Property(s => s.Role).HasDefaultValue(RoleType.Student);
            modelBuilder.Entity<Student>().HasOne(s => s.Resume)
                .WithOne(r => r.Student);
            modelBuilder.Entity<Student>().HasMany(s => s.FeaturedAdvertisements)
                .WithOne(f => f.Student);
            modelBuilder.Entity<Student>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Employer>().HasMany(e => e.Advertisements)
                .WithOne(a => a.Employer).HasForeignKey(a => a.EmployerId);
            modelBuilder.Entity<Employer>().Property(s => s.Role).HasDefaultValue(RoleType.Employer);
            modelBuilder.Entity<Employer>().Property(c => c.Id).ValueGeneratedOnAdd();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
