using System.ComponentModel.DataAnnotations;

namespace AdminApp.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Course number is required")]
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Enter a 4 digit course number")]
        public int CourseNo { get; set; }
        public string Title { get; set; }
        public string Length { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}