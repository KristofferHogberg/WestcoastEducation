using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ApplicationContext _context;
        public AuthController(IConfiguration config, UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
        }

        // [HttpPost("register")]
        // public async Task<ActionResult<ApplicationUserViewModel>> RegisterUser(PostApplicationUserViewModel model)
        // {
        //     var user = new ApplicationUser();
        //     user.Address = new Address();

        //     if (model.TeacherId is not null)
        //     {
        //         user.UserId = model.TeacherId;
        //     }


        //     user.FirstName = model.FirstName!.ToLower();
        //     user.LastName = model.LastName!.ToLower();
        //     user.Email = model.Email!.ToLower();
        //     user.UserName = model.Email!.ToLower();
        //     user.PhoneNumber = model.PhoneNumber!.ToLower();
        //     user.Address!.Street = model.Address!.Street!.ToLower();
        //     user.Address!.City = model.Address!.City!.ToLower();
        //     user.Address!.ZipCode = model.Address!.ZipCode!.ToLower();
        //     user.Address!.Country = model.Address!.Country!.ToLower();

        //     var result = await _userManager.CreateAsync(user);

        //     if (result.Succeeded)
        //     {
        //         return StatusCode(201);
        //     }
        //     else
        //     {
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError("User registration", error.Description);
        //         }
        //         return StatusCode(500, ModelState);
        //     }
        // }
    }
}