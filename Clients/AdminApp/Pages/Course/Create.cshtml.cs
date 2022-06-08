using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Course
{
    public class Create : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public CreateCourseViewModel CourseModel { get; set; }
        [BindProperty]
        public string CategoryFromForm { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<CategoryViewModel> Categories { get; set; }

        public Create(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            Categories = await http.GetFromJsonAsync<List<CategoryViewModel>>(http.BaseAddress + $"/categories/list");
            if (Categories is null)
            {
                return BadRequest();
            }

            foreach (var category in Categories)
            {
                if (CategoryFromForm == category.CategoryName)
                {
                    CourseModel.CategoryId = category.CategoryId;
                }
            }

            var response = await http.PostAsJsonAsync(http.BaseAddress + "/courses/addcourse", CourseModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}