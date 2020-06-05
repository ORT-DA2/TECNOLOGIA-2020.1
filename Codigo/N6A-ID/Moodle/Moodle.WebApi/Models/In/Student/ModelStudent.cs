using System;
using System.ComponentModel.DataAnnotations;
using Moodle.Domain;

namespace Moodle.WebApi
{
    public class ModelStudent
    {
        [Required]
        [MaxLength(50)]
        public string Name { private get; set; }

        [Required]
        [MaxLength(6)]
        public string StudentNumber { private get; set; }

        public Student ToEntity()
        {
            var student = new Student()
            {
                Name = this.Name,
                StudentNumber = this.StudentNumber    
            };

            if(!student.IsValid())
            {
                throw new Exception("Datos basicos incorrectos");
            }

            return student;
        }

        public Student ToUpdateEntity()
        {
            var student = new Student()
            {
                Name = this.Name,
                StudentNumber = this.StudentNumber    
            };

            return student;
        }
    }
}