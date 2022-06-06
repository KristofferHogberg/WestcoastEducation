using Westcoast_Education_Api.ViewModels.Address;

namespace Westcoast_Education_Api.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public int? TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public AddressViewModel? Address { get; set; }
    }
}