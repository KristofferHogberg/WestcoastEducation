using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Teacher
{
    public class Create : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [ViewData]
        public string ErrorMessage { get; set; }
        public List<TeacherViewModel> Teachers { get; set; }
        [BindProperty]
        public CreateTeacherViewModel TeacherModel { get; set; }
        [BindProperty]
        public List<string> CategoriesFromForm { get; set; }

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
            Teachers = await http.GetFromJsonAsync<List<TeacherViewModel>>(http.BaseAddress + "/teachers/list");

            foreach (var student in Teachers)
            {
                if (student.Email == TeacherModel.Email)
                {
                    ErrorMessage = $"{TeacherModel.Email} allready exist in the system";
                    return Page();
                }
            }

            var categories = new List<CategoryViewModel>();

            if (CategoriesFromForm is null)
            {
                ErrorMessage = "Please select a category";
                return Page();
            }

            foreach (var name in CategoriesFromForm)
            {
                categories.Add(new CategoryViewModel
                {
                    CategoryName = name
                });
            }

            TeacherModel.Categories = categories;

            var response = await http.PostAsJsonAsync(http.BaseAddress + "/teachers/register", TeacherModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}