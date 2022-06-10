using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Category;

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
            return await _context.Categories
              .ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<CategoryWithCoursesViewModel>> GetCategoriesWithCoursesAsync()
        {

            return await _context.Categories
                .Include(ca => ca.Courses)
                .ProjectTo<CategoryWithCoursesViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<CategoryWithLimitedCourseViewModel>> GetCategoriesWithLimitedCoursesAsync()
        {
            return await _context.Categories
                .Include(ca => ca.Courses)
                .ProjectTo<CategoryWithLimitedCourseViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<CategoryWithCoursesViewModel> GetCategoryWithCoursesAsync(int id)
        {
            var category = await _context.Categories
                .Include(ca => ca.Courses)
                .Where(c => c.Id == id)
                .ProjectTo<CategoryWithCoursesViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            if (category is null)
            {
                throw new Exception($"Could not find category with id: {id} in the system");
            }

            return category;
        }

        public async Task<CategoryWithLimitedCourseViewModel> GetCategoryWithLimitedCoursesAsync(int id)
        {
            var category = await _context.Categories
                .Include(ca => ca.Courses)
                .Where(c => c.Id == id)
                .ProjectTo<CategoryWithLimitedCourseViewModel>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();

            if (category is null)
            {
                throw new Exception($"Could not find category with id: {id} in the system");
            }

            return category;
        }

        public async Task CreateCategoryAsync(PostCategoryViewModel model)
        {
            var categoryToAdd = new Category { CategoryName = model.CategoryName!.ToUpper() };

            if (categoryToAdd is null)
            {
                throw new Exception($"Could not add category: {model.CategoryName} to the system");
            }

            await _context.Categories.AddAsync(categoryToAdd);

        }

        public async Task DeleteCategoryAsync(int id)
        {
            var response = await _context.Categories.FindAsync(id);

            if (response is null)
            {
                throw new Exception($"We could not find a course with id: {id}");
            }

            _context.Categories.Remove(response);
        }

        public async Task<bool> ExistById(int id)
        {
            return await _context.Courses.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    }
}