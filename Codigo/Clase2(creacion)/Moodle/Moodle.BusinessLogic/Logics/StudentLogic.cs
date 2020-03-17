using System.Collections.Generic;
using Moodle.Domain;

namespace Moodle.BusinessLogic
{
    public class StudentLogic
    {
        public List<Student> GetAll()
        {
            return new List<Student>()
            {
                new Student()
                {
                    Id = 0,
                    Name = "Daniel",
                    StudentNumber = "185082"
                }
            };
        }
    }
}