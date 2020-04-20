using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using Ej.Domain;
using Ej.BL;
using Ej.DA.Interface;
using System.Linq;

namespace Ej.BL.Test
{
    [TestClass]
    public class PersonServiceTest
    {
        [TestMethod]
        public void CreatePersonCaseNotExist()
        {
            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            var mock = new Mock<IManagerDA<Person>>(MockBehavior.Strict);

            mock.Setup(m => m.GetAll()).Returns(() => new List<Person>());
            mock.Setup(m => m.Add(person));
            mock.Setup(m => m.Save());

            var service = new PersonService(mock.Object);
            int result = service.Create(person);

            mock.VerifyAll();
            Assert.AreEqual(result, id);
        }
        [ExpectedException(typeof(Exception), "Ya existe el usuario.")]
        [TestMethod]
        public void CreatePersonCaseExist()
        {
            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            List<Person> list = new List<Person>();
            list.Add(person);
            var mock = new Mock<IManagerDA<Person>>(MockBehavior.Strict);

            mock.Setup(m => m.GetAll()).Returns(list);

            var service = new PersonService(mock.Object);
            int result = service.Create(person);
        }
        [TestMethod]
        public void RemovePerson()
        {
            int id = 1;
            var mock = new Mock<IManagerDA<Person>>(MockBehavior.Strict);

            mock.Setup(m => m.Remove(id));
            mock.Setup(m => m.Save());

            var service = new PersonService(mock.Object);
            service.Remove(id);

            mock.VerifyAll();
        }
        [TestMethod]
        public void UpdatePerson()
        {
            Person person = new Person()
            {
                Id = 1,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            var mock = new Mock<IManagerDA<Person>>(MockBehavior.Strict);

            mock.Setup(m => m.Update(person));
            mock.Setup(m => m.Save());

            var service = new PersonService(mock.Object);
            service.Update(person);

            mock.VerifyAll();
        }
        [TestMethod]
        public void GetAll()
        {
            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            List<Person> list = new List<Person>();
            list.Add(person);
            var mock = new Mock<IManagerDA<Person>>(MockBehavior.Strict);

            mock.Setup(m => m.GetAll()).Returns(list);

            var service = new PersonService(mock.Object);
            List<Person> result = service.GetAll().ToList<Person>();

            mock.VerifyAll();
            Assert.IsTrue(result.Any(p => p.Id == id));
        }
        [TestMethod]
        public void Get()
        {
            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598274563",
                Email = "nico@nico.com"
            };
            var mock = new Mock<IManagerDA<Person>>(MockBehavior.Strict);

            mock.Setup(m => m.Get(id)).Returns(person);

            var service = new PersonService(mock.Object);
            Person result = service.Get(id);

            mock.VerifyAll();
            Assert.AreEqual(result.Id, id);
        }
    }
}
