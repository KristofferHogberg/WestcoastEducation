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
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachersWithCategories()
        {
            return Ok(await _repository.GetTeachersWithcategoriesAsync());
        }

        [HttpPost("register")]
        public async Task<ActionResult> AddUserAsync(PostTeacherViewModel model)
        {
            try
            {
                await _repository.CreateTeacherAsync(model);

                if (await _repository.SaveAllAsync())
                {
                    return StatusCode(201);
                }

                return StatusCode(500, "Could not save the user");
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
    }
}