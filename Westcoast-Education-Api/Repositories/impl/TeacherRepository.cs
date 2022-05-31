using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Address;
using Westcoast_Education_Api.ViewModels.Category;
using Westcoast_Education_Api.ViewModels.Course;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherRepository(ApplicationContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TeacherViewModel>> GetAllTeachersAsync()
        {
            return await _context.ApplicationUsers
                .Include(u => u.Teacher)
                .ThenInclude(u => u!.ApplicationUser!.Address)
                .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();

        }

        public async Task<List<TeacherWithCategoriesViewModel>> GetTeachersWithCategoriesAsync()
        {
            return await _context.ApplicationUsers
                .Include(u => u.Teacher)
                .ThenInclude(c => c!.Categories)
                .ProjectTo<TeacherWithCategoriesViewModel>(_mapper.ConfigurationProvider).ToListAsync();


            // return await _context.Teachers.Include(ca => ca.Categories)
            // .Select(c => new TeacherWithCategoriesViewModel
            // {
            //     FirstName = c.ApplicationUser!.FirstName,
            //     LastName = c.ApplicationUser.LastName,
            //     Categories = c.Categories

            //     .Select(c => new CategoryViewModel
            //     {
            //         CategoryName = c.CategoryName
            //     }).ToList()

            // }).ToListAsync();

        }

        public async Task<List<TeacherWithCategoriesViewModel>> GetTeacherWithCategoriesAsync(int id)
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

        public async Task<List<TeacherWithCoursesViewModel>> GetTeachersWithCoursesAsync()
        {
            return await _context.Teachers.Include(ca => ca.Courses)
            .Select(t => new TeacherWithCoursesViewModel
            {
                FirstName = t.ApplicationUser!.FirstName,
                LastName = t.ApplicationUser.LastName,
                Courses = t.Courses

                .Select(c => new CourseViewModel
                {
                    CourseNo = c.CourseNo,
                    Title = c.Title,
                    Length = c.Length,
                    Description = c.Description,
                    Details = c.Details
                }).ToList()

            }).ToListAsync();

        }



        public async Task<IdentityResult> CreateTeacherAsync(PostTeacherViewModel model)
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
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
            };

            // TODO: Can't add identical addresses.

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

            var availableCategories = await _context.Categories.ToListAsync();
            var categoriesToAdd = new List<Category>();

            foreach (var category in model.Categories)
            {
                if (!await CategoryExistByNameAsync(category.CategoryName!))
                {
                    throw new Exception($"could not find category: {category.CategoryName} in the system");
                }

                if (categoriesToAdd.Where(c => c.CategoryName == category.CategoryName).Any())
                {
                    throw new Exception($"Duplicates not allowed: {category.CategoryName}");
                }

                categoriesToAdd.AddRange(availableCategories
                .Where(c => c.CategoryName == category.CategoryName).ToList());
            }

            var availableCourses = await _context.Courses.ToListAsync();
            var coursesToAdd = new List<Course>();

            foreach (var course in model.Courses)
            {
                if (!await CourseExistByNoAsync(course.CourseNo!))
                {
                    throw new Exception($"could not find course: {course.CourseNo} in the system");
                }

                if (coursesToAdd.Where(c => c.CourseNo == course.CourseNo).Any())
                {
                    throw new Exception($"Duplicates not allowed: {course.CourseNo}");
                }
                coursesToAdd.AddRange(availableCourses
                .Where(c => c.CourseNo == course.CourseNo).ToList());
            }

            var teacherToAdd = new Teacher
            {
                Categories = categoriesToAdd,
                Courses = coursesToAdd
            };

            appUser.Teacher = teacherToAdd;
            return await _userManager.CreateAsync(appUser);
        }

        public async Task<bool> CategoryExistByNameAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryName == name);
        }

        public async Task<bool> CourseExistByNoAsync(int courseNo)
        {
            return await _context.Courses.AnyAsync(c => c.CourseNo == courseNo);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var response = await _context.Teachers.Include(u => u.ApplicationUser)
            .ThenInclude(u => u!.Address)

            .Where(s => s.ApplicationUser!.TeacherId == id).SingleOrDefaultAsync();

            if (response is null)
            {
                throw new Exception($"We could not find teacher with id: {id}");
            }

            // TODO: Fix real cascade with address
            var address = response.ApplicationUser!.Address;

            if (address is null)
            {
                throw new Exception($"We could not find the address: {response.ApplicationUser.AddressId}");
            }

            _context.Teachers.Remove(response);
            _context.Addresses.Remove(address!);
        }
    }
}



