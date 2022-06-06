using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Student
{
    public class Create : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public CreateStudentViewModel StudentModel { get; set; }

        public Create(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public void OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            var response = await http.PostAsJsonAsync(http.BaseAddress + "/students/register", StudentModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}