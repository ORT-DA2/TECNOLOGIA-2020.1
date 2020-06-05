namespace Reflection.Domain
{
    public class Mouth : IFaceValidation
    {
        public bool AllGood()
        {
            System.Console.WriteLine("---AllGood de mouth");
            
            return true;
        }
    }
}