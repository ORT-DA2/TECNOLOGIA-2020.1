using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Homeworks.BusinessLogic;
using Homeworks.Domain;

namespace Homeworks.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private HomeworksLogic homeworksLogic;

        public HomeworksController() {
            homeworksLogic = new HomeworksLogic();
        }

        // GET api/homeworks
        [HttpGet]
        public ActionResult Get()
        {
            List<Homework> homeworks = homeworksLogic.GetHomeworks();
            return Ok(homeworks);
        }
    }
}
