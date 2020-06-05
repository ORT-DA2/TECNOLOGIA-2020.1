using System;
using System.Reflection;
using Reflection.Domain.Interface;

namespace Reflection.Domain
{
    public class Person : IPerson
    {
        public Eye RightEye { get; set; }
        public Eye LeftEye { get; set; }
        public Mouth Mouth { get; set; }
        public Eyebrow RightEyebrow { get; set; }
        public Eyebrow LeftEyebrow { get; set; }
        public Nose Nose { get; set; }
        public Chin Chin { get; set; }
        public Forehead Forehead { get; set; }

        //Con reflection
        public bool AllGood()
        {
            System.Console.WriteLine("--AllGood de person");
            bool valid = true;

           PropertyInfo[] properties = this.GetType().GetProperties();

           for (int i = 0; i < properties.Length && valid; i++)
           {
               IFaceValidation value = properties[i].GetValue(this) as IFaceValidation;

                valid = value.AllGood();
           }
           
           return valid;
        }

        //Sin reflection mal pero no tan mal
        public bool _AllGood(){
            return this.RightEye.AllGood() && this.LeftEye.AllGood() && this.Mouth.AllGood() && this.RightEyebrow.AllGood() && this.Nose.AllGood() && this.Chin.AllGood() && this.Forehead.AllGood();
        }

        //Sin reflection y mal
        public bool _AllGood2(){
            bool good = true;

            //Validacion de RightEye

            //Validacion de LeftEye

            //Validacion de Mouth

            //Validacion de RightEyebrow

            //Validacion de Nose

            //Validacion de chin

            //Validacion de Forehead

            return good;
        }

    }
}
