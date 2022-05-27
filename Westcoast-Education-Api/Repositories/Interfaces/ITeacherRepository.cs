using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<List<TeacherViewModel>> GetAllTeachersAsync();
        public Task<List<TeacherWithCategoriesViewModel>> GetTeachersWithCategoriesAsync();
        public Task<List<TeacherWithCoursesViewModel>> GetTeachersWithCoursesAsync();
        public Task<IdentityResult> CreateTeacherAsync(PostTeacherViewModel model);
        public Task<bool> SaveAllAsync();
    }
}