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
    public class HomeworksController: ControllerBase, IDisposable
    {
        private HomeworksLogic homeworksLogic;

        public HomeworksController() {
            homeworksLogic = new HomeworksLogic();
        }

        // GET api/homeworks
        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<Homework> homeworks = homeworksLogic.GetHomeworks();
            return Ok(homeworks);
        }

        [HttpGet("{id}", Name = "Get")]
        // /api/homeworks/{id}
        public IActionResult Get(Guid id)
        {
            Homework homeWorktoGet=null;
            try {
                homeWorktoGet = homeworksLogic.Get(id);
            }
            catch (Exception e){
                //TODO: Log the problem
            }
           
            if (homeWorktoGet == null) {
                //TODO: Manejar de forma choerente los códigos
                return NotFound();
            }
            return Ok(homeWorktoGet);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Homework homework)
        {
            try {
                Homework createdHomework = homeworksLogic.Create(homework);
                return CreatedAtRoute("Get", new { id = homework.Id }, createdHomework);
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/Exercises", Name = "AddExercise")]
        public IActionResult PostExercise(Guid id, [FromBody] Exercise exercise)
        {
            Exercise createdExercise = homeworksLogic.AddExercise(id, exercise);
            if (createdExercise == null) {
                return BadRequest();
            }
            return CreatedAtRoute("GetExercise", new { id = createdExercise.Id }, createdExercise);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Homework homework)
        {
            try {
                Homework updatedHomework = homeworksLogic.Update(id, homework);
                return CreatedAtRoute("Get", new { id = homework.Id }, updatedHomework);
            } catch(ArgumentException e) {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            homeworksLogic.Remove(id);
            return NoContent();
        }

        public void Dispose()
        {
            homeworksLogic.Dispose();
        }
  }
}
