using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Models;

namespace Westcoast_Education_Api.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
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
            .HasMany(c => c.Students)
            .WithMany(c => c.Courses)
            .UsingEntity<CourseStudents>
            (cs => cs.HasOne<Student>().WithMany(),
            cs => cs.HasOne<Course>().WithMany())
            .Property(cs => cs.EnrollmentDate)
            .HasDefaultValueSql("getdate()");

            builder.Entity<Course>().HasIndex(c => c.CourseNo)
                .IsUnique();

        }
    }
}