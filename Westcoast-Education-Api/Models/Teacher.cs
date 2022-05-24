namespace Westcoast_Education_Api.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public virtual List<Category>? Categories { get; set; } = new List<Category>();

    }
}