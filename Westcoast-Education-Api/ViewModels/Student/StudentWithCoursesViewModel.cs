using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.ViewModels.Student
{
    public class StudentWithCoursesViewModel
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}