using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Student;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<StudentViewModel>>> ListCoursesAsync()
        {
            return Ok(await _repository.GetAllStudentsAsync());
        }

        [HttpGet("courses")]
        public async Task<ActionResult<List<StudentViewModel>>> ListStudentsWithCoursesAsync()
        {
            return Ok(await _repository.GetCourseStudentsRegistriesAsync());
        }

        [HttpPost("register")]
        public async Task<ActionResult> AddUserAsync(PostStudentViewModel model)
        {

            var result = await _repository.CreateStudentAsync(model);

            if (result.Succeeded)
            {
                return StatusCode(201, $"User created: {model.UserName}");
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentAsync(int id)
        {
            try
            {
                await _repository.DeleteStudentAsync(id);

                if (await _repository.SaveAllAsync())
                {
                    return NoContent();
                }
                return StatusCode(500, "Something went wrong");

                // var result = await _repository.DeleteUserAsync(id);

                // if (result.Succeeded)
                // {
                //     return StatusCode(201, $"User deleted: {id}");
                // }
                // else
                // {
                //     foreach (var error in result.Errors)
                //     {
                //         ModelState.AddModelError("User registration", error.Description);
                //     }
                //     return StatusCode(500, ModelState);
                // }
            }
            catch (Exception Ex)
            {
                return StatusCode(500, Ex.Message);
            }
        }
    }
}