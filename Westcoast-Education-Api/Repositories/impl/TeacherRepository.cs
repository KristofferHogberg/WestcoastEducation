using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Category;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public TeacherRepository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TeacherViewModel>> GetAllTeachersAsync()
        {
            var response = await _context.ApplicationUsers.Include(u => u.Address).ToListAsync();
            var teacherList = new List<TeacherViewModel>();

            foreach (var teacher in response)
            {
                teacherList.Add(new TeacherViewModel
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Email = teacher.Email,
                    PhoneNumber = teacher.PhoneNumber,
                    Street = teacher.Address!.Street,
                    City = teacher.Address.City,
                    ZipCode = teacher.Address.ZipCode,
                    Country = teacher.Address.Country
                });
            }
            return teacherList;
        }

        public async Task<List<TeacherWithCategoriesViewModel>> GetTeachersWithcategoriesAsync()
        {
            return await _context.Teachers.Include(ca => ca.Categories)
            .Select(c => new TeacherWithCategoriesViewModel
            {
                FirstName = c.ApplicationUser!.FirstName,
                LastName = c.ApplicationUser.LastName,
                Categories = c.Categories

                .Select(c => new CategoryViewModel
                {
                    CategoryName = c.CategoryName
                }).ToList()

            }).ToListAsync();

        }

        public async Task CreateTeacherAsync(PostTeacherViewModel model)
        {
            var appUser = await _context.ApplicationUsers.Include(a => a.Address)
            .Where(a => a.Email.ToLower() == model.Email!.ToLower()).SingleOrDefaultAsync();

            if (appUser is not null)
            {
                throw new Exception($"User allready exist with email: {model.Email}");
            }

            appUser = new ApplicationUser
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var address = await _context.Addresses
            .Where(a => a.Street == model.Street && a.ZipCode == model.ZipCode).SingleOrDefaultAsync();

            if (address is null)
            {
                address = new Address
                {
                    Street = model.Street,
                    City = model.City,
                    ZipCode = model.ZipCode,
                    Country = model.Country
                };
            }

            appUser.Address = address;

            var teacherToAdd = new Teacher
            {
                ApplicationUser = appUser,
            };

            var categories = new List<Category>();
            foreach (var category in model.Categories)
            {
                categories = await _context.Categories
                .Where(c => c.CategoryName == category.CategoryName).ToListAsync();
            }

            teacherToAdd.Categories = categories;

            await _context.Teachers.AddAsync(teacherToAdd);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}


