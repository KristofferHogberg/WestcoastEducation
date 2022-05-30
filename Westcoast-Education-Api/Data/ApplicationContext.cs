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
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<CourseStudents> CourseStudents => Set<CourseStudents>();
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
                .UsingEntity<CourseStudents>()
                .Property(d => d.EnrollmentDate)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Entity<Course>()
                .HasIndex(c => c.CourseNo)
                .IsUnique();

            builder.Entity<ApplicationUser>()
            .HasOne(u => u.Student)
            .WithOne(s => s.ApplicationUser)
            .OnDelete(DeleteBehavior.ClientCascade);

            // foreach (var key in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            // {
            //     key.DeleteBehavior = DeleteBehavior.Cascade;
            // }

            //     builder.Entity<ApplicationUser>()
            //    .HasOne(u => u.Teacher)
            //    .WithOne(s => s.ApplicationUser)
            //    .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<ApplicationUser>()
           .HasOne(u => u.Address)
           .WithOne(s => s.ApplicationUser)
           .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}