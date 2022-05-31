using Microsoft.AspNetCore.Identity;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<List<TeacherViewModel>> GetAllTeachersAsync();
        public Task<List<TeacherWithCategoriesViewModel>> GetTeachersWithCategoriesAsync();
        public Task<List<TeacherWithCategoriesViewModel>> GetTeacherWithCategoriesAsync(int id);
        public Task<List<TeacherWithCoursesViewModel>> GetTeachersWithCoursesAsync();
        public Task<IdentityResult> CreateTeacherAsync(PostTeacherViewModel model);
        public Task DeleteTeacherAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}