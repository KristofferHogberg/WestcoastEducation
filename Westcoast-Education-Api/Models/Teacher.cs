namespace Westcoast_Education_Api.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public virtual List<Category>? Categories { get; set; } = new List<Category>();

    }
}