using System.ComponentModel;

namespace AdminApp.ViewModels
{
    public class TeacherWithCategoriesViewModel
    {
        [DisplayName("Firstname")]
        public string FirstName { get; set; }
        [DisplayName("Lastname")]
        public string LastName { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}