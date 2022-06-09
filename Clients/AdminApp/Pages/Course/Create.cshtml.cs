using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Course
{
    public class Create : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;

        [ViewData]
        public string ErrorMessage { get; set; }
        [BindProperty]
        public CreateCourseViewModel CourseModel { get; set; }
        [BindProperty]

        public List<CourseViewModel> Courses { get; set; }
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

            Courses = await http.GetFromJsonAsync<List<CourseViewModel>>(http.BaseAddress + $"/courses/list");
            Categories = await http.GetFromJsonAsync<List<CategoryViewModel>>(http.BaseAddress + $"/categories/list");

            foreach (var course in Courses)
            {
                if (CourseModel.CourseNo == course.CourseNo)
                {
                    ErrorMessage = $"Course number: {CourseModel.CourseNo} allredy exist in the system";
                    return Page();
                }
            }

            if (Categories is null)
            {
                ErrorMessage = "Could not fetch categories";
                return Page();
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