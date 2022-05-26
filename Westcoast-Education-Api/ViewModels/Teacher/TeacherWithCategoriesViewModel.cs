using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Westcoast_Education_Api.ViewModels.Category;

namespace Westcoast_Education_Api.ViewModels.Teacher
{
    public class TeacherWithCategoriesViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}