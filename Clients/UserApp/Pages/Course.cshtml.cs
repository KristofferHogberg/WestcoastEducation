using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UserApp.ViewModels;

namespace UserApp.Pages;

public class Course : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _config;

    [BindProperty]
    public List<CourseViewModel>? Courses { get; set; }

    public Course(ILogger<IndexModel> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public async Task OnGetAsync()
    {
        var baseUrl = _config.GetValue<string>("baseUrl");
        var url = $"{baseUrl}/courses/list";

        using var http = new HttpClient();
        Courses = await http.GetFromJsonAsync<List<CourseViewModel>>(url);
    }
}
