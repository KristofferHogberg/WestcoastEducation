using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Student;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;
        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateStudentAsync(PostStudentViewModel model)
        {
            if (await _context.Students.Include(s => s.ApplicationUser).Where(u => u.ApplicationUser!.Email == model.Email).AnyAsync())
            {
                // TODO: fix better message
                throw new Exception("User allready exist");
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = new Address
                {
                    Street = model.Street,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Country = model.Country
                }

            };

            var studentToAdd = new Student
            {
                ApplicationUser = user,
            };

            await _context.Students.AddAsync(studentToAdd);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}