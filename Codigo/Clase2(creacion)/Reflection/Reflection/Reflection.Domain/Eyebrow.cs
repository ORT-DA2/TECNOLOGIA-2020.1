namespace Reflection.Domain
{
    public class Eyebrow : IFaceValidation
    {
        public bool AllGood()
        {
            System.Console.WriteLine("---AllGood de eyebrow");
            
            return true;
        }
    }
}