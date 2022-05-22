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

        public async Task AddCourseAsync(PostCourseViewModel model)
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
                TeacherId = model.TeacherId
            };

            if (courseToAdd is null)
            {
                throw new Exception($"Could not add course: {model.Title} to the system");
            }

            await _context.Courses.AddAsync(courseToAdd);
        }

        public async Task<List<CourseViewModel>> ListAllCoursesAsync()
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
                    CategoryId = course.CategoryId,
                    TeacherId = course.TeacherId
                });
            }

            return coursesList;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}