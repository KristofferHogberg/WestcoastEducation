using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface ITeacherRepository
    {
        public Task CreateTeacherAsync(PostTeacherViewModel model);
        public Task<bool> SaveAllAsync();
    }
}