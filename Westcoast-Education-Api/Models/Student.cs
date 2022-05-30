using System.ComponentModel.DataAnnotations;

namespace Westcoast_Education_Api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public bool IsTeacher { get; set; } = false;
        public ApplicationUser? ApplicationUser { get; set; }
        public virtual List<Course> Courses { get; set; } = new List<Course>();
        public virtual List<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();
    }
}