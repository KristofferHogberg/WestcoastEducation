using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> GetAllCoursesAsync();
        public Task<List<CourseWithCategoryViewModel>> GetCoursesWithCategoryAsync();
        public Task CreateCourseAsync(PostCourseViewModel model);
        public Task UpdateCourseAsync(PatchCourseViewModel model);
        public Task DeleteCourseAsync(int id);
        public Task<bool> ExistByCourseNoAsync(int courseNo);
        public Task CreateCourseStudentRegistryAsync(int courseNo, int studentId);
        public Task<bool> SaveAllAsync();
    }
}