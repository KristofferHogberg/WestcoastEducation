using System.ComponentModel.DataAnnotations;
using Westcoast_Education_Api.ViewModels.Address;

namespace Westcoast_Education_Api.ViewModels.Student
{
    public class PostStudentViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? ZipCode { get; set; }
        [Required]
        public string? Country { get; set; }
        public bool Isteacher { get; set; } = false;
        //public int StudentId { get; set; }

        //public AddressViewModel? Address { get; set; }
    }
}