using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Teacher
{
    public class Edit : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public EditTeacherViewModel TeacherModel { get; set; }
        [BindProperty]
        public List<string> CategoriesFromForm { get; set; }
        [BindProperty]
        public List<CourseViewModel> AvailableCourses { get; set; }
        [BindProperty]
        public List<CourseViewModel> SelectedCourses { get; set; }

        public Edit(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            AvailableCourses = await http.GetFromJsonAsync<List<CourseViewModel>>(http.BaseAddress + $"/courses/list");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var categories = new List<CategoryViewModel>();

            if (CategoriesFromForm is null)
            {
                return BadRequest("No categories was found");
            }

            foreach (var name in CategoriesFromForm)
            {
                categories.Add(new CategoryViewModel
                {
                    CategoryName = name
                });
            }

            TeacherModel.Categories = categories;

            var http = _client.CreateClient("WestEduApi");
            var response = await http.PutAsJsonAsync(http.BaseAddress + $"/teachers/update/{id}", TeacherModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}