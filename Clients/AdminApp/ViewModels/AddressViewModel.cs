using System.ComponentModel.DataAnnotations;

namespace AdminApp.ViewModels
{
    public class AddressViewModel
    {
        public int AddresId { get; set; }
        [Display(Name = "Enter Street address")]
        [Required(ErrorMessage = "Street address is required")]
        public string Street { get; set; }
        [Display(Name = "Enter City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Display(Name = "Zip-code")]
        [Required(ErrorMessage = "Zip-code is required")]
        public string ZipCode { get; set; }
        [Display(Name = "Enter Country")]
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
    }
}