using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Category;
using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            var response = await _context.Categories.ToListAsync();
            var categoryList = new List<CategoryViewModel>();

            // TODO implement AutoMapper
            foreach (var category in response)
            {
                categoryList.Add(new CategoryViewModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.CategoryName
                });
            }
            return categoryList;
        }

        public async Task<List<CategoryWithCoursesViewModel>> GetCategoriesWithCoursesAsync()
        {
            return await _context.Categories.Include(ca => ca.Courses)
            .Select(c => new CategoryWithCoursesViewModel
            {
                CategoryId = c.Id,
                CategoryName = c.CategoryName,
                Courses = c.Courses!

                .Select(c => new CourseViewModel
                {
                    CourseId = c.Id,
                    CourseNo = c.CourseNo,
                    Title = c.Title,
                    Length = c.Length,
                    Description = c.Description,
                    Details = c.Details,
                    CategoryId = c.CategoryId,
                    TeacherId = c.TeacherId

                }).ToList()
            }).ToListAsync();
        }


        public Task AddCategoryAsync(PostCategoryViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}