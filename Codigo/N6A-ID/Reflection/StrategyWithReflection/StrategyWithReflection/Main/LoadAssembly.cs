using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sort.Interface;

namespace Main
{
    public class LoadAssembly
    {
        private readonly DirectoryInfo directory;
        private IEnumerable<Type> implementations;

        public LoadAssembly(string path)
        {
            this.directory = new DirectoryInfo(path);
            this.implementations = new List<Type>();
        }


        public IEnumerable<Type> GetImplementations()
        {
            FileInfo[] files = this.directory.GetFiles("*.dll");
            foreach (var file in files)
            {
                Assembly assemblyLoaded = Assembly.LoadFile(file.FullName);
                var loadedImplementations = assemblyLoaded.GetTypes().Where(t => typeof(ISort).IsAssignableFrom(t) && t.IsClass);

                if (!loadedImplementations.Any())
                {
                    Console.WriteLine("Nadie implementa la interfaz: {0} en el assembly: {1} ", nameof(ISort), file.FullName);
                }
                else
                {
                    this.implementations = this.implementations.Union(loadedImplementations);
                }
            }

            return this.implementations;
        }

        public ISort GetImplementation(int index)
        {
            return Activator.CreateInstance(this.implementations.ElementAt(index)) as ISort;
        }
    }
}