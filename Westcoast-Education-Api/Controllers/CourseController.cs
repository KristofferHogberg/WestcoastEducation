using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels;
using Westcoast_Education_Api.ViewModels.Course;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _repository;
        public CourseController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CourseViewModel>>> ListCoursesAsync()
        {
            return Ok(await _repository.ListAllCoursesAsync());
        }

        [HttpPost("addcourse")]
        public async Task<ActionResult> AddCourse(PostCourseViewModel model)
        {
            try
            {
                await _repository.AddCourseAsync(model);

                if (await _repository.SaveAllAsync())
                {
                    return StatusCode(201);
                }

                return StatusCode(500, "Could not save the course");
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseAsync(int id)
        {
            try
            {
                await _repository.DeleteCourseAsync(id);

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