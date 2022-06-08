using System.ComponentModel.DataAnnotations;

namespace AdminApp.ViewModels
{
    public class TeacherViewModel
    {
        public int? TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AddressViewModel Address { get; set; }
    }
}