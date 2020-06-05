namespace Reflection.Domain
{
    public class Eye : IFaceValidation
    {
        public bool AllGood()
        {
            System.Console.WriteLine("---AllGood de eye");
            
            return true;
        }
    }
}