using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast_Education_Api.Models
{
    public class CourseStudents
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
    }
}