using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Course;
using Westcoast_Education_Api.ViewModels.CourseStudents;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CourseRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            return await _context.Courses.ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<CourseViewModel> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.Where(c => c.Id == id)
            .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();


            if (course is null)
            {
                throw new Exception($"Could not find course: {id} in the system");
            }

            return course;
        }

        public async Task<List<CourseWithCategoryViewModel>> GetCoursesWithCategoryAsync()
        {
            return await _context.Courses
                .Include(c => c.Category)
                .ProjectTo<CourseWithCategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task CreateCourseAsync(PostCourseViewModel model)
        {
            var courseToAdd = _mapper.Map<Course>(model);

            if (courseToAdd is null)
            {
                throw new Exception($"Could not add course: {model.Title} to the system");
            }

            if (!await CategoryExist(model.CategoryId))
            {
                throw new Exception($"could not find category with id: {model.CategoryId} in the system");
            }

            await _context.Courses.AddAsync(courseToAdd);
        }

        public async Task UpdateCourseAsync(int id, PatchCourseViewModel model)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course is null)
            {
                throw new Exception($"We could not find any course with id: {id}");
            }

            _mapper.Map<PatchCourseViewModel, Course>(model, course);
            _context.Courses.Update(course);
        }

        public async Task CreateCourseStudentRegistryAsync(PostCourseStudentsViewModel model)
        {
            // TODO: Implement more efficent verification of registries and duplicate checks?
            var course = await _context.Courses.Where(c => c.CourseNo == model.CourseNo).SingleOrDefaultAsync();

            if (course is null)
            {
                throw new Exception($"Could not find course: {model.CourseNo}");
            }

            var student = await _context.Students.Include(u => u.ApplicationUser).Where(u => u.ApplicationUser!.Email == model.Email).SingleOrDefaultAsync();
            if (student is null)
            {
                throw new Exception($"Could not find student: {model.Email}");
            }

            var registry = new CourseStudents
            {
                CourseId = course.Id,
                StudentId = student.Id,
                EnrollmentDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")
            };

            if (await _context.CourseStudents.Where(c => c.CourseId == registry.CourseId && c.StudentId == registry.StudentId).AnyAsync())
            {
                throw new Exception($"Student: {model.Email} is allredy enrolled in course: {model.CourseNo}");
            }

            await _context.CourseStudents.AddAsync(registry);
        }
        public async Task DeleteCourseAsync(int id)
        {
            var response = await _context.Courses.FindAsync(id);

            if (response is null)
            {
                throw new Exception($"We could not find a course with id: {id}");
            }

            _context.Courses.Remove(response);
        }

        public async Task<bool> CategoryExist(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> TeacherExist(int id)
        {
            return await _context.Teachers.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> ExistByCourseNoAsync(int courseNo)
        {
            return await _context.Courses.AnyAsync(cn => cn.CourseNo == courseNo);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}