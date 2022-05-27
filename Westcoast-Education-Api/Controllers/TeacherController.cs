using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels;
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
    }

}

