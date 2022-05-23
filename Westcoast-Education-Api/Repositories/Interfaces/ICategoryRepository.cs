using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westcoast_Education_Api.ViewModels.Category;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        public Task<List<CategoryWithCoursesViewModel>> GetCategoriesWithCoursesAsync();
        public Task AddCategoryAsync(PostCategoryViewModel model);
        public Task DeleteCategoryAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}