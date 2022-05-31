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

        [HttpGet("courses")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachersWithCoursesAsync()
        {
            return Ok(await _repository.GetTeachersWithCoursesAsync());
        }

        [HttpPost("register")]
        public async Task<ActionResult> AddUserAsync(PostTeacherViewModel model)
        {

            var result = await _repository.CreateTeacherAsync(model);

            if (result.Succeeded)
            {
                return StatusCode(201, $"Teacher created: {model.UserName}");
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

