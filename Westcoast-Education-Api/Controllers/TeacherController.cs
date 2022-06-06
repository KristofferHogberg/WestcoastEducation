using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _repository;
        public TeacherController(ITeacherRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListAllTeachers()
        {
            return Ok(await _repository.GetAllTeachersAsync());
        }

        [HttpGet("categories")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachersWithCategoriesAsync()
        {
            return Ok(await _repository.GetTeachersWithCategoriesAsync());
        }

        [HttpGet("teacher/categories/{id}")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeacherWithCategoriesAsync(int id)
        {
            return Ok(await _repository.GetTeacherWithCategoriesAsync(id));
        }

        [HttpGet("categories/{categoryname}")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachersByCategoryAsync(string categoryName)
        {
            return Ok(await _repository.GetTeachersByCategoryAsync(categoryName));
        }

        [HttpGet("courses")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachersWithCoursesAsync()
        {
            return Ok(await _repository.GetTeachersWithCoursesAsync());
        }


        [HttpGet("courses/{id}")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeacherWithCoursesAsync(int id)
        {
            return Ok(await _repository.GetTeacherWithCoursesAsync(id));
        }

        [HttpPost("register")]
        public async Task<ActionResult> AddUserAsync(PostTeacherViewModel model)
        {

            var result = await _repository.CreateTeacherAsync(model);

            if (result.Succeeded)
            {
                return StatusCode(201, $"Teacher created: {model.Email}");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("User registration", error.Description);
                }
                return StatusCode(500, ModelState);
            }

        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateCourseAsync(int id, PatchTeacherViewModel model)
        {
            try
            {
                await _repository.UpdateTeacherAsync(id, model);

                if (await _repository.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500, $"An error occured when trying to update teacher: {model.Email}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTeachertAsync(int id)
        {
            try
            {
                await _repository.DeleteTeacherAsync(id);

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

