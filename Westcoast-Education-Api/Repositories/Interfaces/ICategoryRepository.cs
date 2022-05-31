using Westcoast_Education_Api.ViewModels.Category;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryViewModel>> GetAllCategoriesAsync();
        public Task<List<CategoryWithCoursesViewModel>> GetCategoriesWithCoursesAsync();
        public Task<CategoryWithCoursesViewModel> GetCategoryWithCoursesAsync(int id);
        public Task CreateCategoryAsync(PostCategoryViewModel model);
        public Task DeleteCategoryAsync(int id);
        public Task<bool> ExistById(int id);
        public Task<bool> SaveAllAsync();
    }
}