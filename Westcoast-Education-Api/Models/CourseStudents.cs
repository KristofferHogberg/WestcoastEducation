namespace Westcoast_Education_Api.Models
{
    public class CourseStudents
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}