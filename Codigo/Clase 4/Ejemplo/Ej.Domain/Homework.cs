using System;

namespace Ej.Domain
{
    public class Homework
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int Score { get; set; }
        public Homework() { }
    }
}
