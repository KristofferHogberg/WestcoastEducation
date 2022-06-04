using Westcoast_Education_Api.ViewModels.Address;

namespace Westcoast_Education_Api.ViewModels.Student
{
    public class StudentViewModel
    {
        public int? StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public AddressViewModel? Address { get; set; }
    }
}