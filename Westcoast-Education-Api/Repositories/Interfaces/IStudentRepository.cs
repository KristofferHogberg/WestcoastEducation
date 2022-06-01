using Microsoft.AspNetCore.Identity;
using Westcoast_Education_Api.ViewModels.Student;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<StudentViewModel>> GetAllStudentsAsync();
        public Task<List<StudentWithCoursesViewModel>> GetCourseStudentsRegistriesAsync();
        public Task<IdentityResult> CreateStudentAsync(PostStudentViewModel model);
        public Task UpdateStudentAsync(int id, PatchStudentViewModel model);
        public Task DeleteStudentAsync(int id);
        public Task<bool> SaveAllAsync();
    }
}