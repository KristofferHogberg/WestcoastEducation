using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.ViewModels.Category
{
    public class CategoryWithCoursesViewModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

    }
}