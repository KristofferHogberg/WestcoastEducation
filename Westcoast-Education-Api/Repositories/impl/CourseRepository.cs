using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Course;

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
            //TODO implement AutoMapper 
            var response = await _context.Courses.ToListAsync();
            var coursesList = new List<CourseViewModel>();

            foreach (var course in response)
            {
                coursesList.Add(
                new CourseViewModel
                {
                    CourseId = course.Id,
                    CourseNo = course.CourseNo,
                    Title = course.Title,
                    Length = course.Length,
                    Description = course.Description,
                    Details = course.Details,
                    CategoryId = course.CategoryId

                });
            }

            return coursesList;
        }

        public async Task<List<CourseWithCategoryViewModel>> GetCoursesWithCategoryAsync()
        {
            //TODO implement AutoMapper 
            return await _context.Courses.Include(c => c.Category)
            .Select(c => new CourseWithCategoryViewModel
            {
                CourseId = c.Id,
                CourseNo = c.CourseNo,
                Title = c.Title,
                Length = c.Length,
                Description = c.Description,
                Details = c.Details,
                CategoryName = c.Category!.CategoryName

            }).ToListAsync();

        }

        public async Task CreateCourseAsync(PostCourseViewModel model)
        {

            // TODO implement AutoMapper
            var courseToAdd = new Course
            {
                CourseNo = model.CourseNo,
                Title = model.Title,
                Length = model.Length,
                Description = model.Description,
                Details = model.Details,
                CategoryId = model.CategoryId,

            };

            if (courseToAdd is null)
            {
                throw new Exception($"Could not add course: {model.Title} to the system");
            }

            if (!await CategoryExist(courseToAdd.CategoryId))
            {
                throw new Exception($"could not find category with id: {model.CategoryId} in the system");
            }
            await _context.Courses.AddAsync(courseToAdd);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var response = await _context.Courses.FindAsync(id);

            if (response is null)
            {
                throw new Exception("We could not find a course with id: {id}");
            }

            _context.Courses.Remove(response);
        }


        public async Task<bool> CategoryExist(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
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