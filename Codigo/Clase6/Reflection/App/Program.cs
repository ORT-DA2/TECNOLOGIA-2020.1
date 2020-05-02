using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Interface;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            InspectAssembly();
            InstantiateObjectUnsecure();
            InstantiateObjectWithKnownInterface();
        }

        private static void InspectAssembly()
        {
            // Cargamos el assembly de ejemplo en memoria
			// Esto debería quedar hecho de forma fácil que el origen no sea
			// necesario recompilar para cambiarlo
            var dllFile = new FileInfo(@"Library.dll");
            string fullPathName = dllFile.FullName;
            Assembly myAssembly = Assembly.LoadFile(fullPathName);
            // Podemos ver que Tipos hay dentro del assembly
            foreach (Type tipo in myAssembly.GetTypes())
            {
                Console.WriteLine(string.Format("Clase: {0}", tipo.Name));

                Console.WriteLine("Fields");
                foreach (FieldInfo prop in tipo.GetFields())
                {
                    Console.WriteLine(string.Format("\t{0} : {1}", prop.Name, prop.FieldType.Name));
                }

                Console.WriteLine("Propiedades");
                foreach (PropertyInfo prop in tipo.GetProperties())
                {
                    Console.WriteLine(string.Format("\t{0} : {1}", prop.Name, prop.PropertyType.Name));
                }

                Console.WriteLine("Constructores");
                foreach (ConstructorInfo con in tipo.GetConstructors())
                {
                    Console.Write("\tConstructor: ");
                    foreach (ParameterInfo param in con.GetParameters())
                    {
                        Console.Write(string.Format("{0} : {1} ", param.Name, param.ParameterType.Name));
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("Metodos");
                foreach (MethodInfo met in tipo.GetMethods())
                {
                    Console.Write(string.Format("\t{0} ", met.Name));
                    foreach (ParameterInfo param in met.GetParameters())
                    {
                        Console.Write(string.Format("{0} : {1} ", param.Name, param.ParameterType.Name));
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        private static void InstantiateObjectUnsecure()
        {
            // Cargamos el assembly en memoria
            var dllFile = new FileInfo(@"Library.dll");
            Assembly myAssembly = Assembly.LoadFile(dllFile.FullName);

            // Obtenemos el tipo que representa a User
            Type actorType = myAssembly.GetType("Library.Actor");

            // Creamos una instancia de User
            //object actorInstance = Activator.CreateInstance(actorType);

            // O también podemos crearlo pasandole parámetros
            object actorInstance = Activator.CreateInstance(actorType, new object[] { "Juan", "T." });

            // Lo mostramos
            Console.WriteLine(actorInstance.ToString());

            // Invocamos al método
            MethodInfo met = actorType.GetMethod("SayHello");
            Console.WriteLine(met.Invoke(actorInstance, new object[] { "Maria" }));

            // También podemos cambiar su nombre
            PropertyInfo prop = actorType.GetProperty("Firstname");
            Console.WriteLine(prop.GetValue(actorInstance)); // Antes de cambiar el nombre
            prop.SetValue(actorInstance, "Manuel", null);
            Console.WriteLine(prop.GetValue(actorInstance)); // Despues de cambiar el nombre

            Console.ReadLine();
        }

        private static void InstantiateObjectWithKnownInterface()
        {
            // Cargamos el assembly en memoria
            var dllFile = new FileInfo(@"Library.dll");
            Assembly myAssembly = Assembly.LoadFile(dllFile.FullName);
            IEnumerable<Type> implementations = GetTypesInAssembly<IPerson>(myAssembly);
            IPerson person = (IPerson)Activator.CreateInstance(implementations.First(), new object[] { "Juan", "T." });

            Console.WriteLine(person.SayHello("Carlos"));
        }

        private static IEnumerable<Type> GetTypesInAssembly<Interface>(Assembly assembly)
        {
            List<Type> types = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(Interface).IsAssignableFrom(type))
                    types.Add(type);
            }
            return types;
        }

    }
}
