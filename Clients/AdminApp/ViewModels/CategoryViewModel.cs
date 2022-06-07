using System.ComponentModel;

namespace AdminApp.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [DisplayName("Competence(s)")]
        public string CategoryName { get; set; }
    }
}