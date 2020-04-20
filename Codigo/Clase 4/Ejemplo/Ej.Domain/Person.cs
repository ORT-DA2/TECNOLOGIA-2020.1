using System;

namespace Ej.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Person()
        {
            if (this.Name == "" || this.Surname == "" || this.Phone == "" || this.Email == "")
            {
                throw new ArgumentException("Persona mal ingresada");
            }
        }
    }
}
