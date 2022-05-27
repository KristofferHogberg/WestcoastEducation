using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast_Education_Api.ViewModels.CourseStudents
{
    public class PostCourseStudentsViewModel
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        [Required]
        public int CourseNo { get; set; }
        public DateTime? EnrollmentDate { get; set; }
    }
}