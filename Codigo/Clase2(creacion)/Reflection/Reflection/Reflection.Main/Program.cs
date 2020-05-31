using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reflection.Domain;
using Reflection.Domain.Interface;

namespace Reflection.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Person homero = new Person(){
                RightEye = new Eye(),
                LeftEye = new Eye(),
                Forehead = new Forehead(),
                Chin = new Chin(),
                LeftEyebrow = new Eyebrow(),
                Nose = new Nose(),
                RightEyebrow = new Eyebrow(),
                Mouth = new Mouth()
            };

            System.Console.WriteLine("-Empezando autoanalizar a homero: ");
            System.Console.WriteLine(homero.AllGood());


            System.Console.WriteLine("Empezando analizar a lisa de un tercero: ");

            //Configuration.GetSection("Tercero")["Persona"]
            /*
            "Tercero":{
                "Persona":"C:\\Users\\Daniel\\Documents\\My projects\\Reflection\\ImplementacionExterna\\bin\\Debug\\netstandard2.0\\ImplementacionExterna.dll"
            }
            */
            Assembly assemblyLoaded = Assembly.LoadFile("C:\\Users\\Daniel\\Documents\\My projects\\Reflection\\ImplementacionExterna\\bin\\Debug\\netstandard2.0\\ImplementacionExterna.dll");
            IEnumerable<Type> implementations = assemblyLoaded.GetTypes().Where(t => typeof(IPerson).IsAssignableFrom(t));

            if(!implementations.Any())
            {
                System.Console.WriteLine("Nadie implementa la interfaz: {0} en el assembly: {1} ",nameof(IPerson), "C:\\Users\\Daniel\\Documents\\My projects\\Reflection\\ImplementacionExterna\\bin\\Debug\\netstandard2.0\\ImplementacionExterna.dll");
            }
            else
            {
                foreach (var implementation in implementations)
                {
                    var concreteImplementation = Activator.CreateInstance(implementation) as IPerson;

                    concreteImplementation.AllGood();        
                }
            }

            System.Console.ReadLine();
        }
    }
}
