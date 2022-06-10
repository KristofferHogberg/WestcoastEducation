using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages.Register
{
    public class RegisterStudent : PageModel
    {
        private readonly ILogger<RegisterStudent> _logger;
        private readonly IHttpClientFactory _client;
        [ViewData]
        public string ErrorMessage { get; set; }
        [ViewData]
        public string Verification { get; set; }
        [BindProperty]
        public CreateStudentViewModel StudentModel { get; set; }
        public List<StudentViewModel> Students { get; set; }

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
            Students = await http.GetFromJsonAsync<List<StudentViewModel>>(http.BaseAddress + "/students/list");

            foreach (var student in Students)
            {
                if (student.Email == StudentModel.Email)
                {
                    ErrorMessage = $"{StudentModel.Email} allready exist in the system";
                }
            }

            var response = await http.PostAsJsonAsync(http.BaseAddress + "/students/register", StudentModel);

            if (response.IsSuccessStatusCode)
            {
                ErrorMessage = "";
                Verification = "Succsess, welcome to Westcoast Education!";
            }

        }
    }
}