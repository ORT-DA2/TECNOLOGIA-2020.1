using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Ej.Domain;
using Ej.DA;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;

namespace Ej.DA.Test
{
    [TestClass]
    public class PersonManagerDATest
    {
        [TestMethod]
        public void AddPerson()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            Person person = new Person()
            {
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            using (var context = new EjContext(options))
            {
                var manager = new PersonManagerDA(context);
                manager.Add(person);
                manager.Save();
                Assert.AreEqual(manager.GetAll().ToList().Count, 1);
                context.Set<Person>().Remove(person);
                context.SaveChanges();
            }
        }
        [TestMethod]
        public void RemovePersonExist()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            using (var context = new EjContext(options))
            {
                context.Set<Person>().Add(person);
                context.SaveChanges();
                var manager = new PersonManagerDA(context);
                manager.Remove(id);
                manager.Save();
                Assert.AreEqual(manager.GetAll().ToList().Count, 0);
            }
        }
        [ExpectedException(typeof(KeyNotFoundException), "La persona no existe")]
        [TestMethod]
        public void RemovePersonNotExist()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            using (var context = new EjContext(options))
            {
                var manager = new PersonManagerDA(context);
                manager.Remove(id);
                manager.Save();
            }
        }
        [TestMethod]
        public void UpdatePersonExist()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            using (var context = new EjContext(options))
            {
                context.Set<Person>().Add(person);
                context.SaveChanges();
                var manager = new PersonManagerDA(context);
                person.Name = "Braulio";
                manager.Update(person);
                manager.Save();
                Assert.AreEqual(manager.Get(id).Name, "Braulio");
                context.Set<Person>().Remove(person);
                context.SaveChanges();
            }
        }
        [ExpectedException(typeof(KeyNotFoundException), "La persona no existe")]
        [TestMethod]
        public void UpdatePersonNotExist()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            using (var context = new EjContext(options))
            {
                var manager = new PersonManagerDA(context);
                manager.Update(person);
                manager.Save();
            }
        }
        [TestMethod]
        public void GetAll()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            using (var context = new EjContext(options))
            {
                context.Set<Person>().Add(person);
                context.SaveChanges();
                var manager = new PersonManagerDA(context);
                List<Person> list = manager.GetAll().ToList();
                Assert.AreEqual(list.Count, 1);
                context.Set<Person>().Remove(person);
                context.SaveChanges();
            }
        }
        [TestMethod]
        public void GetByIdExist()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            using (var context = new EjContext(options))
            {
                context.Set<Person>().Add(person);
                context.SaveChanges();
                var manager = new PersonManagerDA(context);
                Person result = manager.Get(id);
                Assert.AreEqual(result, person);
                context.Set<Person>().Remove(person);
                context.SaveChanges();
            }
        }
        [TestMethod]
        public void GetByIdNotExist()
        {
            var options = new DbContextOptionsBuilder<EjContext>()
            .UseInMemoryDatabase(databaseName: "EjDataBase")
            .Options;

            int id = 1;
            using (var context = new EjContext(options))
            {
                var manager = new PersonManagerDA(context);
                Person result = manager.Get(id);
                Assert.AreEqual(result, null);
            }
        }
    }
}
