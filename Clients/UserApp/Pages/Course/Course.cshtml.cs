using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages.Course
{
    public class Course : PageModel
    {
        private readonly ILogger<Course> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public List<CourseViewModel> Courses { get; set; }

        public Course(ILogger<Course> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task OnGetAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            Courses = await http.GetFromJsonAsync<List<CourseViewModel>>(http.BaseAddress + $"/courses/list");

        }
    }
}