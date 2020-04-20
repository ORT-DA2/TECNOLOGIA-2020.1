using System;
using System.Collections.Generic;

namespace Moodle.Domain
{
    public class Student
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("Nombre incorrecto");
                }
                this.name = value;
            }
        }
        public string StudentNumber { get; set; }
        public string Password { get; set; }
        public Guid Token { get; set; }

        public virtual List<StudentCourse> Courses { get; set; }

        public Student()
        {
            this.Courses = new List<StudentCourse>();
        }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(this.Name) && !String.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrEmpty(this.StudentNumber);
        }
    }
}