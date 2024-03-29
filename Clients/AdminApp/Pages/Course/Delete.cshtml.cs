using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Course
{
    public class Delete : PageModel
    {
        private readonly ILogger<Delete> _logger;
        private readonly IHttpClientFactory _client;

        public Delete(ILogger<Delete> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            var response = await http.DeleteAsync(http.BaseAddress + $"/courses/delete/{id}");

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }
        }

    }
}