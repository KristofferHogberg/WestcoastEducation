using System.ComponentModel.DataAnnotations;

namespace AdminApp.ViewModels
{
    public class CreateCourseViewModel
    {
        [Display(Name = "Enter Course Number")]
        [Required(ErrorMessage = "Course number is required")]
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Enter a 4 digit course number")]
        public int CourseNo { get; set; }
        [Display(Name = "Enter Title")]
        [Required(ErrorMessage = "Course title is required")]
        public string Title { get; set; }
        [Display(Name = "Enter Length")]
        [Required(ErrorMessage = "Course length is required")]
        public string Length { get; set; }
        [Display(Name = "Enter Description")]
        [Required(ErrorMessage = "Course description is required")]
        public string Description { get; set; }
        [Display(Name = "Enter Details")]
        [Required(ErrorMessage = "Course details is required")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Course category id is required")]
        public int CategoryId { get; set; }
    }
}