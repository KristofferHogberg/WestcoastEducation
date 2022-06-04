using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages;

public class Course : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _config;
    private readonly IHttpClientFactory _client;

    [BindProperty]
    public List<CourseViewModel> Courses { get; set; } = new List<CourseViewModel>();

    public Course(ILogger<IndexModel> logger, IConfiguration config, IHttpClientFactory client)
    {
        _logger = logger;
        _config = config;
        _client = client;
    }

    public async Task OnGetAsync()
    {
        var baseUrl = _config.GetValue<string>("baseUrl");
        var url = $"{baseUrl}/courses/list";

        var http = _client.CreateClient();
        Courses = await http.GetFromJsonAsync<List<CourseViewModel>>(url);
    }
}
