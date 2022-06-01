using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels;
using Westcoast_Education_Api.ViewModels.Course;
using Westcoast_Education_Api.ViewModels.CourseStudents;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _repository;
        public CourseController(ICourseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CourseViewModel>>> ListCoursesAsync()
        {
            return Ok(await _repository.GetAllCoursesAsync());
        }

        [HttpGet("category")]
        public async Task<ActionResult<List<CourseViewModel>>> ListCoursesWithCategoryAsync()
        {
            return Ok(await _repository.GetCoursesWithCategoryAsync());
        }

        [HttpPost("addcourse")]
        public async Task<ActionResult> AddCourseAsync(PostCourseViewModel model)
        {
            try
            {
                if (await _repository.ExistByCourseNoAsync(model.CourseNo))
                {
                    return BadRequest($"Course with id: {model.CourseNo} allready exist in the system");
                }

                await _repository.CreateCourseAsync(model);

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

        [HttpPost("enroll")]
        public async Task<ActionResult> EnrollInCourse(PostCourseStudentsViewModel model)
        {
            await _repository.CreateCourseStudentRegistryAsync(model);
            if (await _repository.SaveAllAsync())
            {
                return StatusCode(201);

            }
            return StatusCode(500, $"Could not enroll in course {model.CourseNo}");
        }

        [HttpPatch("update/{id}")]
        public async Task<ActionResult> UpdateCourseAsync(int id, PatchCourseViewModel model)
        {
            try
            {
                await _repository.UpdateCourseAsync(id, model);

                if (await _repository.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500, $"An error occured when trying to update course: {model.Title} id: {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
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