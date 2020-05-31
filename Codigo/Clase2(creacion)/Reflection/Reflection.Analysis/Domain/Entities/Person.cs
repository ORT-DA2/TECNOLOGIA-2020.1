using System;

namespace Domain
{
    public class Person
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        private bool Adult()
        {
            return this.Age >= 18;
        }
    }
}
