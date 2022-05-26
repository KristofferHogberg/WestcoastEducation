using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westcoast_Education_Api.ViewModels.ApplicationUser;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface IApplicationUserRepository
    {
        public Task<List<ApplicationUserViewModel>> GetAllUsersAsync();
        public Task<ApplicationUserViewModel> GetUserAsync(int id);
        public Task<List<ApplicationUserWithCoursesViewModel>> GetUsersWithCoursesAsync();
        public Task CreateUserAsync(PostApplicationUserViewModel model);
        public Task DeleteUserAsync(int id);
        public Task<bool> ExistById(int id);
        public Task<bool> SaveAllAsync();
    }
}