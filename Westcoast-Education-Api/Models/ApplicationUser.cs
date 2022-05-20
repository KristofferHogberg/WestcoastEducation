using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Westcoast_Education_Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
    }
}