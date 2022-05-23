using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Category;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CategoryViewModel>>> ListAllCategories()
        {
            return await _repository.GetAllCategoriesAsync();
        }

        [HttpGet("courses")]
        public async Task<ActionResult> ListCategoriesWithCourses()
        {
            return Ok(await _repository.GetCategoriesWithCoursesAsync());
        }
    }
}