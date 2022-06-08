using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages.Category
{
    public class CategoryWithCourse : PageModel
    {
        private readonly ILogger<CategoryWithCourse> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public CategoryWithCoursesViewModel CategoryWithCourses { get; set; }

        public CategoryWithCourse(ILogger<CategoryWithCourse> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            CategoryWithCourses = await http.GetFromJsonAsync<CategoryWithCoursesViewModel>(http.BaseAddress + $"/categories/courses/{id}");

        }
    }
}
