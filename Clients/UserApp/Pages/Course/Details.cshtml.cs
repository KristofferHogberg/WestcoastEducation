using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages.Course
{
    public class Details : PageModel
    {
        private readonly ILogger<Details> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public DetailsViewModel DetailsModel { get; set; }

        public Details(ILogger<Details> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            DetailsModel = await http.GetFromJsonAsync<DetailsViewModel>(http.BaseAddress + $"/courses/category/{id}");

            // if (!response.IsSuccessStatusCode)
            // {
            //     string reason = await response.Content.ReadAsStringAsync();
            //     Console.WriteLine(reason);
            // }
        }
    }
}