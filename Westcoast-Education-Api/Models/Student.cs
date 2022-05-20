using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Westcoast_Education_Api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

    }
}