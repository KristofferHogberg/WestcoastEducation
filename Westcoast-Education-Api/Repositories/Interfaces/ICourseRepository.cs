using Westcoast_Education_Api.ViewModels.Course;
using Westcoast_Education_Api.ViewModels.CourseStudents;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> GetAllCoursesAsync();
        public Task<CourseViewModel> GetCourseByIdAsync(int id);
        public Task<List<CourseWithCategoryViewModel>> GetCoursesWithCategoryAsync();
        public Task<CourseWithCategoryViewModel> GetCourseWithCategoryAsync(int id);
        public Task CreateCourseAsync(PostCourseViewModel model);
        public Task CreateCourseStudentRegistryAsync(PostCourseStudentsViewModel model);
        public Task UpdateCourseAsync(int id, PatchCourseViewModel model);
        public Task DeleteCourseAsync(int id);
        public Task<bool> ExistByCourseNoAsync(int courseNo);
        public Task<bool> SaveAllAsync();
    }
}