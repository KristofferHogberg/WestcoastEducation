using Microsoft.AspNetCore.Identity;

namespace Westcoast_Education_Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public int AddressId { get; set; }

        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
        public Address? Address { get; set; }
    }
}