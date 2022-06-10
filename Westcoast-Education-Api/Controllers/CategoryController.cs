using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels;
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
        [HttpGet("courses/limited")]
        public async Task<ActionResult> ListCategoriesWithLimitedCourses()
        {
            return Ok(await _repository.GetCategoriesWithLimitedCoursesAsync());
        }

        [HttpGet("courses/{id}")]
        public async Task<ActionResult> ListCategoryWithCoursesAsync(int id)
        {
            return Ok(await _repository.GetCategoryWithCoursesAsync(id));
        }

        [HttpGet("courses/limited/{id}")]
        public async Task<ActionResult> ListCategoryWithLimitedCoursesAsync(int id)
        {
            return Ok(await _repository.GetCategoryWithLimitedCoursesAsync(id));
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddCategoryAsync(PostCategoryViewModel model)
        {
            try
            {
                await _repository.CreateCategoryAsync(model);

                if (await _repository.SaveAllAsync())
                {
                    return StatusCode(201);
                }
                return StatusCode(500, "Could not save the category");
            }
            catch (Exception Ex)
            {
                var error = new ErrorViewModel
                {
                    StatusCode = 500,
                    StatusText = Ex.Message
                };
                return StatusCode(500, error);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                await _repository.DeleteCategoryAsync(id);

                if (await _repository.SaveAllAsync())
                {
                    return NoContent();
                }
                return StatusCode(500, "Something went wrong");
            }
            catch (Exception Ex)
            {
                return StatusCode(500, Ex.Message);
            }
        }
    }
}