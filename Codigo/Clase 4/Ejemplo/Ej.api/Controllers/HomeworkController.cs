using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ej.BL.Interface;
using Ej.Domain;
namespace Ej.api.Controllers
{
    [ApiController]
    [Route("Homeworks")]
    public class HomeworkController : ControllerBase
    {
        private IHomeworkService Service;
        public HomeworkController(IHomeworkService service) : base()
        {
            this.Service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Service.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Homework person = Service.Get(id);
            if (person == null) return NotFound("No existe la tarea con id: " + id);
            else return Ok(person);
        }
        [HttpPost()]
        public IActionResult Post([FromBody]Homework homework)
        {
            int id = Service.Create(homework);
            return Ok();
        }
    }
}
