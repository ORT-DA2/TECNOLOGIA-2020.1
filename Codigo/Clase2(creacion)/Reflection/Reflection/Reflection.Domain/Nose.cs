namespace Reflection.Domain
{
    public class Nose : IFaceValidation
    {
        public bool AllGood()
        {
            System.Console.WriteLine("---AllGood de nose");
            
            return true;
        }
    }
}