using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Course
{
    public class Edit : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [ViewData]
        public string ErrorMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public CourseViewModel CourseModel { get; set; }
        [BindProperty]
        public List<CourseViewModel> Courses { get; set; }
        [BindProperty]
        public EditCourseViewModel CourseToUpdate { get; set; }
        [BindProperty]
        public string CategoryFromForm { get; set; }

        private int _courseId;

        //[BindProperty(SupportsGet = true)]
        public List<CategoryViewModel> Categories { get; set; }

        public Edit(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public async Task OnGetAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            CourseModel = await http.GetFromJsonAsync<CourseViewModel>(http.BaseAddress + $"/courses/{id}");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            Courses = await http.GetFromJsonAsync<List<CourseViewModel>>(http.BaseAddress + $"/courses/list");
            Categories = await http.GetFromJsonAsync<List<CategoryViewModel>>(http.BaseAddress + $"/categories/list");

            _courseId = id;

            if (CourseModel is null)
            {
                ErrorMessage = "Could not fetch courses";
                return Page();
            }

            foreach (var course in Courses)
            {

                if (course.CourseId != _courseId)
                {
                    if (CourseModel.CourseNo == course.CourseNo)
                    {
                        ErrorMessage = $"Course number: {CourseModel.CourseNo} allredy exist in the system";
                        return Page();
                    }
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

            CourseToUpdate.CourseNo = CourseModel.CourseNo;
            CourseToUpdate.Title = CourseModel.Title;
            CourseToUpdate.Length = CourseModel.Length;
            CourseToUpdate.Description = CourseModel.Description;
            CourseToUpdate.Details = CourseModel.Details;
            CourseToUpdate.CategoryId = CourseModel.CategoryId;

            var response = await http.PutAsJsonAsync(http.BaseAddress + $"/courses/update/{id}", CourseToUpdate);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                ErrorMessage = "Something went wrong...";
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}