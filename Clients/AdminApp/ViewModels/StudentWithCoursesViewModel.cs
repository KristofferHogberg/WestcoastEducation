using System.ComponentModel.DataAnnotations;

namespace AdminApp.ViewModels
{
    public class StudentWithCoursesViewModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CourseNo { get; set; }
        public string Title { get; set; }
        public string EnrollmentDate { get; set; }
    }
}