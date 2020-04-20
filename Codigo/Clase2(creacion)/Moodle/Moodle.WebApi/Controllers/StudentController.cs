using System;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Moodle.BusinessLogic.Interface;

namespace Moodle.WebApi.Controllers
{
    [EnableCors("AllowEverything")]
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentLogic studentLogic;

        public StudentController(IStudentLogic _studentLogic)
        {
            this.studentLogic = _studentLogic;
        }

        //GET api/students
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.studentLogic.GetAll().Select(s => new ModelStudentBasicInfo(s)));
        }

        //api/students
        [HttpGet(Name = "GetAllStudentsFromCourseDa2")]
        [AuthenticationFilter]
        public IActionResult GetAllByCondition()
        {
            return Ok(this.studentLogic.GetAllByCondition(s => s.Courses.Exists(c => c.Course.Name == "DA2")).Select(s => new ModelStudentBasicInfo(s)));
        }


        //api/students/1
        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult Get(int id)
        {
            var student = this.studentLogic.Get(id);

            if (student is null)
            {
                return NotFound("Estudiante no encontrado");
            }

            return Ok(new ModelStudentDetailInfo(student));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ModelStudent modelStudent)
        {
            try
            {
                var student = this.studentLogic.Add(modelStudent.ToEntity());

                return CreatedAtRoute(routeName: "GetStudent",
                                        routeValues: new { id = student.Id },
                                        value: new ModelStudentDetailInfo(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ModelStudent modelStudent)
        {
            try
            {
                this.studentLogic.Update(id, modelStudent.ToUpdateEntity());

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.studentLogic.Delete(id);
            return Ok();
        }
    }
}
