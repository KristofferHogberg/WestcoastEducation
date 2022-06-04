using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages
{
    public class RegisterStudent : PageModel
    {
        private readonly ILogger<RegisterStudent> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public CreateStudentViewModel StudentModel { get; set; }

        public RegisterStudent(ILogger<RegisterStudent> logger, IConfiguration config, IHttpClientFactory client)
        {
            _client = client;
            _config = config;
            _logger = logger;
        }

        public void OnGetAsync()
        {
        }

        public async Task OnPostAsync()
        {
            var http = _client.CreateClient();

            var baseUrl = _config.GetValue<string>("baseUrl");
            var url = $"{baseUrl}/students/register";
            var response = await http.PostAsJsonAsync(url, StudentModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

        }
    }
}