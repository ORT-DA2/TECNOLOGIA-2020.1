using System.Collections.Generic;

namespace Moodle.Domain
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public Course()
        {
            this.Students = new List<Student>();
        }
    }
}