namespace Westcoast_Education_Api.ViewModels.Course
{
    public class CourseWithCategoryViewModel
    {
        public int CourseId { get; set; }
        public int CourseNo { get; set; }
        public string? Title { get; set; }
        public string? Length { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int CategoryId { get; set; }
        public int TeacherId { get; set; }
        public string? CategoryName { get; set; }

    }
}