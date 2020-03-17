using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moodle.BusinessLogic;

namespace Moodle.WebApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly StudentLogic studentLogic;

        public StudentController()
        {
            this.studentLogic = new StudentLogic();
        }

        //GET api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(this.studentLogic.GetAll());
        }
    }
}
