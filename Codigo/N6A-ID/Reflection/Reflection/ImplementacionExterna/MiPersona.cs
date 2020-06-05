

using Reflection.Domain.Interface;

namespace ImplementacionExterna
{
    public class MiPersona : IPerson
    {
        public bool AllGood()
        {
            System.Console.WriteLine("Implementacion de persona");
            return true;
        }
    }
}
