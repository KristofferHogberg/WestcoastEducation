namespace AdminApp.ViewModels
{
    public class TeacherWithCategoriesViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}