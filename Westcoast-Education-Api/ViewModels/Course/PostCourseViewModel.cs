using System.ComponentModel.DataAnnotations;

namespace Westcoast_Education_Api.ViewModels.Course
{
    public class PostCourseViewModel
    {
        [Required(ErrorMessage = "Course number is required")]
        public int CourseNo { get; set; }
        [Required(ErrorMessage = "Course title is required")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Course length is required")]
        public string? Length { get; set; }
        [Required(ErrorMessage = "Course description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Course details is required")]
        public string? Details { get; set; }
        [Required(ErrorMessage = "Course category is required")]
        public int CategoryId { get; set; }
        public int TeacherId { get; set; }
    }
}