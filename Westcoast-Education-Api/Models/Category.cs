namespace Westcoast_Education_Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public virtual List<Course> Courses { get; set; } = new List<Course>();
        public virtual List<Teacher> Teachers { get; set; } = new List<Teacher>();

    }
}