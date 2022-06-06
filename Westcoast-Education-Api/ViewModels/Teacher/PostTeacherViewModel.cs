using System.ComponentModel.DataAnnotations;
using Westcoast_Education_Api.ViewModels.Category;
using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.ViewModels.Teacher
{
    public class PostTeacherViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? ZipCode { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public virtual List<CategoryViewModel>? Categories { get; set; } = new List<CategoryViewModel>();
        //public virtual List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();
    }
}