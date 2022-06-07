using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Teacher
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public List<TeacherViewModel> Teachers { get; set; }
        [BindProperty]
        public CategoryViewModel CategoryModel { get; set; }

        public Index(ILogger<Index> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task OnGetAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            Teachers = await http.GetFromJsonAsync<List<TeacherViewModel>>(http.BaseAddress + $"/teachers/list");

            // if (!response.IsSuccessStatusCode)
            // {
            //     string reason = await response.Content.ReadAsStringAsync();
            //     Console.WriteLine(reason);
            // }
        }


    }
}