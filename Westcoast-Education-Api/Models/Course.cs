namespace Westcoast_Education_Api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseNo { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public virtual List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public virtual List<Student> Students { get; set; } = new List<Student>();
        public virtual List<CourseStudents> CourseStudents { get; set; } = new List<CourseStudents>();

    }
}