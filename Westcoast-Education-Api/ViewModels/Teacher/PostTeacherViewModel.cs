using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast_Education_Api.ViewModels.Teacher
{
    public class PostTeacherViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
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
    }
}