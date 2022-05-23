using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Westcoast_Education_Api.Models;

namespace Westcoast_Education_Api.ViewModels.ApplicationUser
{
    public class PostApplicationUserViewModel
    {
        public int? UserId { get; set; }

        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public Address? Address { get; set; }

        public int? StudentId { get; set; }

        public int? TeacherId { get; set; }
    }
}