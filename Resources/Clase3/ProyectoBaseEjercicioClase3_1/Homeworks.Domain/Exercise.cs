using System;

namespace Homeworks.Domain
{
    public class Exercise
    {
        public Guid Id {get; set;}
        public string Problem {get; set;}
        public int Score {get; set;}

        public Exercise() {
            Id = Guid.NewGuid();
        }
    }
}