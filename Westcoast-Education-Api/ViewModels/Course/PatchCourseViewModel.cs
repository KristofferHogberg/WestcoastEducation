using System.ComponentModel.DataAnnotations;

namespace Westcoast_Education_Api.ViewModels.Course
{
    public class PatchCourseViewModel
    {
        [Required]
        public int CourseNo { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Length { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Details { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}