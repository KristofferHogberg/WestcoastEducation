using System.ComponentModel.DataAnnotations;

namespace AdminApp.ViewModels
{
    public class CreateCourseViewModel
    {
        [Required(ErrorMessage = "Course number is required")]
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Enter a 4 digit course number")]
        public int CourseNo { get; set; }
        [Required(ErrorMessage = "Course title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Course length is required")]
        public string Length { get; set; }
        [Required(ErrorMessage = "Course description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Course details is required")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Course category id is required")]
        public int CategoryId { get; set; }
    }
}