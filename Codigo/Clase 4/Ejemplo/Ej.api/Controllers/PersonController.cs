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
    [Route("Personas")]
    public class PersonController : ControllerBase
    {
        private IPersonService Service;
        public PersonController(IPersonService service) : base()
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
            Person person = Service.Get(id);
            if (person == null) return NotFound("No existe persona con id: " + id);
            else return Ok(person);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Person person)
        {
            try
            {
                person.Id = id;
                Service.Update(person);
                return Ok("Persona actualizada");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost()]
        public IActionResult Post([FromBody]Person person)
        {
            int id = Service.Create(person);
            return Ok("Se creo la persona con id: " + id);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Service.Remove(id);
                return Ok("Se elimino la persona");
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
