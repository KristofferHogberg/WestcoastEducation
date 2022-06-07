using System.ComponentModel;
using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminApp.Pages.Teacher
{
    public class SearchCompetence : PageModel
    {
        private readonly ILogger<SearchCompetence> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public List<TeacherWithCategoriesViewModel> Teachers { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } = string.Empty;

        [BindProperty]
        public string ResponseString { get; set; } = string.Empty;
        public string comma = " ";

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
                categoryNames.Add(category.CategoryName.ToLower());
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                string result = SearchString.TrimStart().TrimEnd().ToLower();

                if (!categoryNames.Contains(result))
                {
                    ResponseString = "No Matches";
                }
                else
                {
                    Teachers = await http.GetFromJsonAsync<List<TeacherWithCategoriesViewModel>>(http.BaseAddress + $"/teachers/categories/{result}");
                }
            }
            else
            {
                ResponseString = "No Matches";
            }
        }
    }
}