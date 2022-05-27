using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels;
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
    }
}