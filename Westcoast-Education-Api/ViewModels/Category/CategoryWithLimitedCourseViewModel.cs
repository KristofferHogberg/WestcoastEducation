using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.ViewModels.Category
{
    public class CategoryWithLimitedCourseViewModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<CourseLimitedViewModel> Courses { get; set; } = new List<CourseLimitedViewModel>();
    }
}