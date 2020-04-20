using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ej_filters.api.Models;
using ej_filters.api.Filters;
using ej_filters.api.Logic;
using ej_filters.auth;

namespace ej_filters.api.Controllers
{
    [ExampleExceptionFilter]
    [ApiController]
    [Route("[controller]")]
    public class JokesController : ControllerBase
    {
        private Auth auth;
        public JokesController() : base()
        {
            ServiceLogic.Startup();
            auth = new Auth();
        }
        [ExampleAuthorizationFilter("No puede ver chistes porque ")]
        [HttpGet]
        public IActionResult GetAll([FromHeader(Name = "auth")]string token)
        {
            return Ok(ServiceLogic.GetJokes());
        }
        [HttpGet("filter")]
        public IActionResult GetByType([FromQuery] string type)
        {
            return Ok(ServiceLogic.GetJokes().Where(x => x.Type.ToUpper() == type.ToUpper()));
        }
        [ExampleActionFilter]
        [HttpPost]
        public IActionResult Post([FromBody] Joke joke)
        {
            ServiceLogic.AddJoke(joke);
            return Ok();
        }
        [ExampleResultFilter]
        [HttpGet("Hello")]
        public IActionResult Hello()
        {
            return Ok("como va?");
        }
        [HttpGet("Excep")]
        public IActionResult GetExcept()
        {
            throw new Exception("Ups rompi algo...");
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            string token = auth.Login(user);
            if(token == "") return NotFound("No existe el usuario: " + user.Nickname);
            else return Ok(token);
        }

    }
}
