using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.ViewModels.Teacher
{
    public class TeacherWithCoursesViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}