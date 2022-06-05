using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages.Category
{
    public class Category : PageModel
    {
        private readonly ILogger<Category> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public List<CategoryViewModel> Categories { get; set; }

        public Category(ILogger<Category> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            Categories = await http.GetFromJsonAsync<List<CategoryViewModel>>(http.BaseAddress + "/categories/list");

        }
    }
}