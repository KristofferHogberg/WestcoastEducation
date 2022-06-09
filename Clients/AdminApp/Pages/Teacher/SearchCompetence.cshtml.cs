using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Teacher
{
    public class SearchCompetence : PageModel
    {
        private readonly ILogger<SearchCompetence> _logger;
        private readonly IHttpClientFactory _client;
        [ViewData]
        public string ErrorMessage { get; set; } = "";

        [BindProperty]
        public List<TeacherWithCategoriesViewModel> Teachers { get; set; } = new List<TeacherWithCategoriesViewModel>();

        public List<CategoryViewModel> Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string separator = " ";

        public SearchCompetence(ILogger<SearchCompetence> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task OnGetAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            Categories = await http.GetFromJsonAsync<List<CategoryViewModel>>(http.BaseAddress + $"/categories/list");

            var categoryNames = new List<string>();
            foreach (var category in Categories)
            {
                categoryNames.Add(category.CategoryName);
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                Result = SearchString.TrimStart().TrimEnd().ToUpper();

                if (!categoryNames.Contains(Result.ToUpper()))
                {
                    ErrorMessage = "No matches";
                }

                Teachers = await http.GetFromJsonAsync<List<TeacherWithCategoriesViewModel>>(http.BaseAddress + $"/teachers/categories/{Result}");

                if (Teachers.Count() == 0)
                {
                    ErrorMessage = "No matches";
                }
            }
        }
    }
}