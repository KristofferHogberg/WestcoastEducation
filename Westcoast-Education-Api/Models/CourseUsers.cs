using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast_Education_Api.Models
{
    public class CourseUsers
    {
        public int CourseId { get; set; }
        public int ApplicationUserId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
    }
}