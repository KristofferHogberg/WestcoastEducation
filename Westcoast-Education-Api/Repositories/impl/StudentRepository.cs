using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Course;
using Westcoast_Education_Api.ViewModels.Student;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public StudentRepository(ApplicationContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<StudentViewModel>> GetAllStudentsAsync()
        {
            //TODO implement AutoMapper
            return await _context.Students.Include(u => u.ApplicationUser).ThenInclude(u => u!.Address)
            .Select(s => new StudentViewModel
            {
                FirstName = s.ApplicationUser!.FirstName,
                LastName = s.ApplicationUser.LastName,
                Email = s.ApplicationUser.Email,
                PhoneNumber = s.ApplicationUser.PhoneNumber,

                Street = s.ApplicationUser.Address!.Street,
                City = s.ApplicationUser.Address.City,
                ZipCode = s.ApplicationUser.Address.ZipCode,
                Country = s.ApplicationUser.Address.Country

            }).ToListAsync();

        }

        public async Task<List<StudentWithCoursesViewModel>> GetCourseStudentsRegistriesAsync()
        {
            return await _context.CourseStudents
            .Include(s => s.Student)
            .ThenInclude(u => u!.ApplicationUser)
            .Select(s => new StudentWithCoursesViewModel
            {
                CourseNo = s.Course!.CourseNo,
                Title = s.Course.Title,
                EnrollmentDate = DateTime.UtcNow,
                FirstName = s.Student!.ApplicationUser!.FirstName,
                LastName = s.Student.ApplicationUser.LastName,
                Email = s.Student.ApplicationUser.Email

            }).ToListAsync();

        }
        public async Task<IdentityResult> CreateStudentAsync(PostStudentViewModel model)
        {
            if (await _context.Students.Include(s => s.ApplicationUser).Where(u => u.ApplicationUser!.Email == model.Email).AnyAsync())
            {
                // TODO: fix better message
                throw new Exception("User allready exist");
            }

            var appUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,

                Student = new Student
                {
                    Isteacher = model.Isteacher,
                },

                Address = new Address
                {
                    Street = model.Street,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Country = model.Country
                }

            };

            return await _userManager.CreateAsync(appUser);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}