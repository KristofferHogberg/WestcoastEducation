using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApp.ViewModels
{
    public class CreateStudentViewModel
    {
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phonenumber is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Street address is required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}