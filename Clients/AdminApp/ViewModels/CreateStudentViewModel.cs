using System.ComponentModel.DataAnnotations;

namespace AdminApp.ViewModels
{
    public class CreateStudentViewModel
    {
        [Display(Name = "Enter First Name")]
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Display(Name = "Enter Last Name")]
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Display(Name = "Enter Phonenumber")]
        [Required(ErrorMessage = "Phonenumber is required")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Enter Street Address")]
        [Required(ErrorMessage = "Street address is required")]
        public string Street { get; set; }
        [Display(Name = "Enter City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Display(Name = "Enter Zip-code")]
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }
        [Display(Name = "Enter Country")]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}