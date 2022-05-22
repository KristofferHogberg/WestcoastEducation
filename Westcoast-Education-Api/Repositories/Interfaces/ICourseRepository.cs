using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseViewModel>> ListAllCoursesAsync();
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task<bool> SaveAllAsync();
    }
}