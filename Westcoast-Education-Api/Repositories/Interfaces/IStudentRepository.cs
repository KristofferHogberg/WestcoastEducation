using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westcoast_Education_Api.ViewModels.Student;

namespace Westcoast_Education_Api.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        public Task CreateStudentAsync(PostStudentViewModel model);
        public Task<bool> SaveAllAsync();
    }
}