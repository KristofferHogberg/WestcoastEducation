using System.ComponentModel.DataAnnotations;

namespace Westcoast_Education_Api.ViewModels.CourseStudents
{
    public class PostCourseStudentsViewModel
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        [Required]
        public int CourseNo { get; set; }
        [Required]
        public string? Email { get; set; }
        public DateTime? EnrollmentDate { get; set; }
    }
}