namespace Reflection.Domain
{
    public class Forehead : IFaceValidation
    {
        public bool AllGood()
        {
            System.Console.WriteLine("---AllGood de forehead");
            
            return true;
        }
    }
}