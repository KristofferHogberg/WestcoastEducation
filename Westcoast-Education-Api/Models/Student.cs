namespace Westcoast_Education_Api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public virtual List<Course>? Courses { get; set; } = new List<Course>();

    }
}