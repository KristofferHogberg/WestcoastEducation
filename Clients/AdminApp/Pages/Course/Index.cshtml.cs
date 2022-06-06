using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Course
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public List<CourseViewModel> Courses { get; set; }


        public Index(ILogger<Index> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task OnGetAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            Courses = await http.GetFromJsonAsync<List<CourseViewModel>>(http.BaseAddress + $"/courses/category");

            // if (!response.IsSuccessStatusCode)
            // {
            //     string reason = await response.Content.ReadAsStringAsync();
            //     Console.WriteLine(reason);
            // }
        }
    }
}