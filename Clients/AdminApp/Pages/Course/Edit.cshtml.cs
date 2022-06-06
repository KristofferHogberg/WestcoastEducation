using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Course
{
    public class Edit : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public EditCourseViewModel CourseModel { get; set; }

        public Edit(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            var response = await http.PutAsJsonAsync(http.BaseAddress + $"/courses/update/{id}", CourseModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}