using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Westcoast_Education_Api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseNo { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int CategoryId { get; set; }
        public int TeacherId { get; set; }



    }
}