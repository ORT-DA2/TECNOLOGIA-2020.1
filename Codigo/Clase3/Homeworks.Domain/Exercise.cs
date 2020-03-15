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

        public bool IsValid()
        {
            return true;
        }

        public Exercise Update(Exercise entity)
        {
            if (entity.Problem != null) 
                Problem = entity.Problem;
            return this;
        }
    }
}