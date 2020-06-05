using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Assembly assemblyLoaded = Assembly.LoadFile("C:\\Users\\Daniel\\Documents\\Personal\\ORT\\Clase DA2\\Reflection\\Reflection.Analysis\\Domain\\bin\\Debug\\netstandard2.0\\Domain.dll");

            IEnumerable<Type> types = assemblyLoaded.GetTypes().Where(c => c.IsPublic);

            Console.WriteLine("Public classes in the assembly: " + assemblyLoaded.GetName().Name);
            foreach (var type in types)
            {
                Console.WriteLine("-" + type.Name);
                Console.WriteLine("|-Functions (public):");

                IEnumerable<MethodInfo> publicFunctions = type.GetMethods().Where(f => f.IsPublic);
                if (publicFunctions.Any())
                {
                    foreach (var function in publicFunctions)
                    {
                        Console.WriteLine(" |-" + function.Name);
                    }
                }
                else
                {
                    Console.WriteLine(" |-No public functions");
                }

                Console.WriteLine("|-Functions (not public):");

                IEnumerable<MethodInfo> privateFunctions = type.GetMethods().Where(f => f.IsPrivate);
                if (privateFunctions.Any())
                {
                    foreach (var function in privateFunctions)
                    {
                        Console.WriteLine(" |-" + function.Name);
                    }
                }
                else
                {
                    Console.WriteLine(" |-No private functions");
                }

            }

            IEnumerable<Type> notPublicTypes = assemblyLoaded.GetTypes().Where(c => c.IsNotPublic);

            Console.WriteLine();
            Console.WriteLine("Not public classes in the assembly: " + assemblyLoaded.GetName().Name);
            foreach (var type in notPublicTypes)
            {
                Console.WriteLine("-" + type.Name);
                Console.WriteLine("|-Functions (public):");

                IEnumerable<MethodInfo> publicFunctions = type.GetMethods().Where(f => f.IsPublic);
                if (publicFunctions.Any())
                {
                    foreach (var function in publicFunctions)
                    {
                        Console.WriteLine(" |-" + function.Name);
                    }
                }
                else
                {
                    Console.WriteLine(" |-No public functions");
                }

                Console.WriteLine("|-Functions (not public):");

                IEnumerable<MethodInfo> privateFunctions = type.GetMethods(BindingFlags.NonPublic);
                if (privateFunctions.Any())
                {
                    foreach (var function in privateFunctions)
                    {
                        Console.WriteLine(" |-" + function.Name);
                    }
                }
                else
                {
                    Console.WriteLine(" |-No private functions");
                }

            }

            Console.ReadLine();
        }
    }
}
