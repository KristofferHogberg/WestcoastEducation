using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Course
{
    public class Edit : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty(SupportsGet = true)]
        public CourseViewModel Course { get; set; }
        [BindProperty]
        public EditCourseViewModel CourseToUpdate { get; set; }

        public Edit(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            Course = await http.GetFromJsonAsync<CourseViewModel>(http.BaseAddress + $"/courses/{id}");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (Course is null)
            {
                return BadRequest();
            }

            CourseToUpdate.CourseNo = Course.CourseNo;
            CourseToUpdate.Title = Course.Title;
            CourseToUpdate.Length = Course.Length;
            CourseToUpdate.Description = Course.Description;
            CourseToUpdate.Details = Course.Details;
            CourseToUpdate.CategoryId = Course.CategoryId;

            var http = _client.CreateClient("WestEduApi");
            var response = await http.PutAsJsonAsync(http.BaseAddress + $"/courses/update/{id}", CourseToUpdate);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}