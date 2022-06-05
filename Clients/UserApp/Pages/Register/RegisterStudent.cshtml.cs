using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages.Register
{
    public class RegisterStudent : PageModel
    {
        private readonly ILogger<RegisterStudent> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public CreateStudentViewModel StudentModel { get; set; }

        public RegisterStudent(ILogger<RegisterStudent> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public void OnGetAsync()
        {
        }

        public async Task OnPostAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            var response = await http.PostAsJsonAsync(http.BaseAddress + "/students/register", StudentModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

        }
    }
}