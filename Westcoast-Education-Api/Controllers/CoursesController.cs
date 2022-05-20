using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Westcoast_Education_Api.Models;

namespace Westcoast_Education_Api.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : Controller
    {
        [HttpGet()]
        public void ListCourses()
        {

        }
    }
}