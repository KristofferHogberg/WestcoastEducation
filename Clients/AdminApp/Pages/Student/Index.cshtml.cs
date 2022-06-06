using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AdminApp.Pages.Student
{
    public class Index : PageModel
    {
        private readonly ILogger<Index> _logger;
        private readonly IHttpClientFactory _client;

        [BindProperty]
        public List<StudentViewModel> Students { get; set; }

        public Index(ILogger<Index> logger, IHttpClientFactory client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task OnGetAsync()
        {
            var http = _client.CreateClient("WestEduApi");
            Students = await http.GetFromJsonAsync<List<StudentViewModel>>(http.BaseAddress + $"/students/list");

            // if (!response.IsSuccessStatusCode)
            // {
            //     string reason = await response.Content.ReadAsStringAsync();
            //     Console.WriteLine(reason);
            // }
        }
    }
}