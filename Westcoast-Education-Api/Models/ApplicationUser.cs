using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Westcoast_Education_Api.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int TeacherId { get; set; }
        public int AddressId { get; set; }

        public Teacher? Teacher { get; set; }
        public Address? Address { get; set; }
        public virtual List<Course>? Courses { get; set; } = new List<Course>();
    }
}