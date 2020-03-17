using System.Collections.Generic;

namespace Moodle.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StudentNumber { get; set; }

        public List<Course> Courses { get; set; }

        public Student()
        {
            this.Courses = new List<Course>();
        }
    }
}