using System;
using Interface;

namespace Library
{
    public class Actor : IPerson
    {
        public string Firstname { get; set; }
        private string surname;

        public string Name { get { return  Firstname + surname; } }

        public Actor(string aName, string aSurname) {
            Firstname = aName;
            surname = aSurname;
        }

        public string SayHello(string to) {
            return "Hellooo! " + to;
        }
    }
}
