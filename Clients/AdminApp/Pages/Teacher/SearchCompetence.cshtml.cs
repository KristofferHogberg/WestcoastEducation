using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Teacher
{
    public class SearchCompetence : PageModel
    {
        private readonly ILogger<SearchCompetence> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public List<TeacherWithCategoriesViewModel> Teachers { get; set; }

        public SearchCompetence(ILogger<SearchCompetence> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task OnGetAsync(string categoryName)
        {
            var http = _client.CreateClient("WestEduApi");
            Teachers = await http.GetFromJsonAsync<List<TeacherWithCategoriesViewModel>>(http.BaseAddress + $"/teachers/categories/Java");

            // if (!response.IsSuccessStatusCode)
            // {
            //     string reason = await response.Content.ReadAsStringAsync();
            //     Console.WriteLine(reason);
            // }
        }
    }
}