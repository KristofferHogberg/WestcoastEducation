using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast_Education_Api.ViewModels.Student
{
    public class StudentWithCoursesViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int CourseNo { get; set; }
        public string? Title { get; set; }
        public DateTime? EnrollmentDate { get; set; }
    }
}