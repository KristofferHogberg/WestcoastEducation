using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels;
using Westcoast_Education_Api.ViewModels.ApplicationUser;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserRepository _repository;
        public ApplicationUserController(IApplicationUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> AddUserAsync(PostApplicationUserViewModel model)
        {
            try
            {
                await _repository.CreateUserAsync(model);

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