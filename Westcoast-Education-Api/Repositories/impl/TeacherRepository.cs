using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationContext _context;

        public TeacherRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateTeacherAsync(PostTeacherViewModel model)
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
            var teacherToAdd = new Teacher
            {
                ApplicationUser = user

            };

            await _context.Teachers.AddAsync(teacherToAdd);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}