using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Models;

namespace Westcoast_Education_Api.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasMany(c => c.Teachers)
                .WithMany(t => t.Categories)
                .UsingEntity<CategoryTeachers>
                (ct => ct.HasOne<Teacher>().WithMany(),
                ct => ct.HasOne<Category>().WithMany());

            builder.Entity<Category>().HasIndex(c => c.CategoryName)
                .IsUnique();

            builder.Entity<Course>()
            .HasMany(c => c.Users)
            .WithMany(c => c.Courses)
            .UsingEntity<CourseUsers>
            (cu => cu.HasOne<ApplicationUser>().WithMany(),
            cu => cu.HasOne<Course>().WithMany())
            .Property(cs => cs.EnrollmentDate)
            .HasDefaultValueSql("getdate()");

            builder.Entity<Course>().HasIndex(c => c.CourseNo)
                .IsUnique();

        }
    }
}