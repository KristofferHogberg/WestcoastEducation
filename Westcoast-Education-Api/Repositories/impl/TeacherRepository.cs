using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
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
                .Where(u => u.TeacherId != null)
                .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TeacherViewModel> GetTeacherByIdAsync(int id)
        {
            var teacher = await _context.ApplicationUsers
                .Include(u => u.Teacher)
                .ThenInclude(u => u!.ApplicationUser!.Address)
                .Where(u => u.TeacherId == id)
                .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();

            if (teacher is null)
            {
                throw new Exception($"Could not find teacher: {id} in the system");
            }

            return teacher;
        }

        public async Task<List<TeacherWithCategoriesViewModel>> GetTeachersWithCategoriesAsync()
        {
            return await _context.ApplicationUsers
                .Include(u => u.Teacher)
                .ThenInclude(c => c!.Categories)
                .Where(u => u.TeacherId != null)
                .ProjectTo<TeacherWithCategoriesViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TeacherWithCategoriesViewModel> GetTeacherWithCategoriesAsync(int id)
        {
            var teacher = await _context.ApplicationUsers
               .Include(u => u.Teacher)
               .ThenInclude(c => c!.Categories)
               .Where(u => u.TeacherId != null && u.TeacherId == id)
               .ProjectTo<TeacherWithCategoriesViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            if (teacher is null)
            {
                throw new Exception($"Could not find teacher: {id} in the system");
            }

            return teacher;
        }

        public async Task<List<TeacherWithCategoriesViewModel>> GetTeachersByCategoryAsync(string categoryName)
        {
            var teacher = await _context.ApplicationUsers
               .Include(u => u.Teacher)
               .ThenInclude(c => c!.Categories)
               .Where(u => u.Teacher!.Categories.Any(c => c.CategoryName == categoryName))
               .ProjectTo<TeacherWithCategoriesViewModel>(_mapper.ConfigurationProvider).ToListAsync();

            if (teacher.Count() == 0)
            {
                throw new Exception($"Could not find teacher: {categoryName} in the system");
            }

            return teacher;
        }

        public async Task<List<TeacherWithCoursesViewModel>> GetTeachersWithCoursesAsync()
        {
            return await _context.ApplicationUsers
               .Include(u => u.Teacher)
               .ThenInclude(c => c!.Courses)
               .Where(u => u.TeacherId != null)
               .ProjectTo<TeacherWithCoursesViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TeacherWithCoursesViewModel> GetTeacherWithCoursesAsync(int id)
        {
            var teacher = await _context.ApplicationUsers
               .Include(u => u.Teacher)
               .ThenInclude(c => c!.Courses)
               .Where(u => u.TeacherId != null && u.TeacherId == id)
               .ProjectTo<TeacherWithCoursesViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            if (teacher is null)
            {
                throw new Exception($"Could not find teacher: {id} in the system");
            }

            return teacher;
        }

        public async Task<IdentityResult> CreateTeacherAsync(PostTeacherViewModel model)
        {
            var teacher = _mapper.Map<Teacher>(model);
            var address = _mapper.Map<Address>(model);
            var appUser = _mapper.Map<ApplicationUser>(model);

            appUser.Teacher = teacher;
            appUser.Address = address;
            appUser.UserName = model.Email;

            if (appUser is null)
            {
                throw new Exception($"Could not create teacher: {model.Email}");
            }

            var categoriesToAdd = await AddTeacherCategoriesAsync(model);

            appUser.Teacher.Categories = categoriesToAdd;
            return await _userManager.CreateAsync(appUser);
        }

        public async Task<List<Category>> AddTeacherCategoriesAsync(PostTeacherViewModel model)
        {
            var availableCategories = await _context.Categories.ToListAsync();
            var categoriesToAdd = new List<Category>();

            if (availableCategories is null)
            {
                throw new Exception($"Could not create fetch: categories from the database");
            }

            foreach (var category in model.Categories!)
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
            return categoriesToAdd;
        }

        public async Task UpdateTeacherAsync(int id, PatchTeacherViewModel model)
        {
            var appUser = await _context.ApplicationUsers
               .Include(u => u.Teacher!.Categories)
               .Include(u => u.Address)
               .Where(s => s.TeacherId == id)
               .SingleOrDefaultAsync();

            if (appUser is null)
            {
                throw new Exception($"Could not find student: {model.Email}");
            }

            _mapper.Map<PatchTeacherViewModel, ApplicationUser>(model, appUser);
            _mapper.Map<PatchTeacherViewModel, Address>(model, appUser.Address!);

            if (appUser is null)
            {
                throw new Exception($"Could not create teacher: {model.Email}");
            }

            var categoriesToAdd = await UpdateTeacherCategoriesAsync(model);
            var coursesToAdd = await UpdateTracherToCoursesAsync(model);
            appUser.Teacher!.Categories = categoriesToAdd;
            appUser.Teacher!.Courses = coursesToAdd;
            _context.ApplicationUsers.Update(appUser);
        }

        public async Task<List<Category>> UpdateTeacherCategoriesAsync(PatchTeacherViewModel model)
        {
            var availableCategories = await _context.Categories.ToListAsync();
            var categoriesToUpdate = new List<Category>();

            if (availableCategories is null)
            {
                throw new Exception($"Could not create fetch: categories from the database");
            }

            foreach (var category in model.Categories!)
            {
                if (!await CategoryExistByNameAsync(category.CategoryName!))
                {
                    throw new Exception($"could not find category: {category.CategoryName} in the system");
                }

                if (categoriesToUpdate.Where(c => c.CategoryName == category.CategoryName).Any())
                {
                    throw new Exception($"Duplicates not allowed: {category.CategoryName}");
                }

                categoriesToUpdate.AddRange(availableCategories
                .Where(c => c.CategoryName == category.CategoryName).ToList());
            }
            return categoriesToUpdate;
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

        // TODO: Check for logical errors!
        public async Task<List<Course>> UpdateTracherToCoursesAsync(PatchTeacherViewModel model)
        {
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
                Courses = coursesToAdd
            };
            return coursesToAdd;
        }

    }

}




