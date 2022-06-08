using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Student
{
    public class Edit : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty(SupportsGet = true)]
        public StudentViewModel Student { get; set; }
        [BindProperty]
        public EditStudentViewModel StudentToUpdate { get; set; }

        public Edit(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            Student = await http.GetFromJsonAsync<StudentViewModel>(http.BaseAddress + $"/students/{id}");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (Student is null)
            {
                return BadRequest();
            }

            StudentToUpdate.FirstName = Student.FirstName;
            StudentToUpdate.LastName = Student.LastName;
            StudentToUpdate.Email = Student.Email;
            StudentToUpdate.PhoneNumber = Student.PhoneNumber;
            StudentToUpdate.Street = Student.Address.Street;
            StudentToUpdate.City = Student.Address.City;
            StudentToUpdate.ZipCode = Student.Address.ZipCode;
            StudentToUpdate.Country = Student.Address.Country;

            var http = _client.CreateClient("WestEduApi");
            var response = await http.PutAsJsonAsync(http.BaseAddress + $"/students/update/{id}", StudentToUpdate);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}