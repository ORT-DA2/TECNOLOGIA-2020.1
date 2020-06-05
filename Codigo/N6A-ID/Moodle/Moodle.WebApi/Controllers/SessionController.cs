using System;
using Microsoft.AspNetCore.Mvc;
using Moodle.BusinessLogic.Interface;

namespace Moodle.WebApi
{
    
    [ApiController]
    [Route("api/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic sessionLogic;

        public SessionController(ISessionLogic _sessionLogic)
        {
            this.sessionLogic = _sessionLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody]ModelUserLogin modelUserLogin)
        {
            try
            {
                return Ok(this.sessionLogic.Login(modelUserLogin.Credential, modelUserLogin.Password));
            }
            catch(Exception)
            {
                return BadRequest("Error credentials");
            }
        }
    }
}