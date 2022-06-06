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
    public class Edit : PageModel
    {
        private readonly ILogger<Create> _logger;
        private readonly IHttpClientFactory _client;
        [BindProperty]
        public EditStudentViewModel StudentModel { get; set; }

        public Edit(ILogger<Create> logger, IHttpClientFactory client)
        {
            _client = client;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var http = _client.CreateClient("WestEduApi");
            var response = await http.PutAsJsonAsync(http.BaseAddress + $"/students/update/{id}", StudentModel);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await response.Content.ReadAsStringAsync();
                Console.WriteLine(reason);
            }

            return RedirectToPage("Index");

        }
    }
}