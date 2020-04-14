using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Moq;
using Ej.Domain;
using Ej.api;
using Ej.api.Controllers;
using System;
using Ej.BL.Interface;

namespace Ej.api.Test
{
    [TestClass]
    public class PersonControllerTest
    {
        #region Get()
        [TestMethod]
        public void GetAllPersonsCaseEmpty()
        {
            var listReturn = new List<Person>();
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.GetAll()).Returns(listReturn);
            var controller = new PersonController(mock.Object);

            var result = controller.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<Person>;

            mock.VerifyAll();
            Assert.AreEqual(value, listReturn);
        }
        [TestMethod]
        public void GetAllPersonsCaseNotEmpty()
        {
            var listReturn = new List<Person>();
            Person person1 = new Person()
            {
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598123456",
                Email = "nico@nico.com"
            };
            Person person2 = new Person()
            {
                Name = "Pepe",
                Surname = "Argento",
                Phone = "+598654789",
                Email = "pp@argento.com"
            };
            listReturn.Add(person1);
            listReturn.Add(person2);
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.GetAll()).Returns(listReturn);
            var controller = new PersonController(mock.Object);

            var result = controller.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<Person>;

            mock.VerifyAll();
            Assert.AreEqual(value, listReturn);
        }
        #endregion

        #region Get(id)
        [TestMethod]
        public void GetPersonsByIdCaseExist()
        {
            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598123456",
                Email = "nico@nico.com"
            };
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.Get(id)).Returns(person);
            var controller = new PersonController(mock.Object);

            var result = controller.GetById(id);
            var okResult = result as ObjectResult;
            var value = okResult.Value as Person;

            mock.VerifyAll();
            Assert.AreEqual(value, person);
        }
        [TestMethod]
        public void GetPersonsByIdCaseNotExist()
        {
            int id = 2;
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.Get(id)).Returns(() => null);
            var controller = new PersonController(mock.Object);

            var result = controller.GetById(id);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;
            var value = okResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(value, "No existe persona con id: " + id);
            Assert.AreEqual(statusCode, 404);
        }
        #endregion

        #region Put(id)
        [TestMethod]
        public void PutCasetExist()
        {
            int id = 1;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598123456",
                Email = "nico@nico.com"
            };
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.Update(person));
            var controller = new PersonController(mock.Object);

            var result = controller.Put(id, person);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(statusCode, 200);
        }
        [TestMethod]
        public void PutCaseNotExist()
        {
            int id = 2;
            Person person = new Person()
            {
                Id = id,
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598123456",
                Email = "nico@nico.com"
            };
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.Update(person)).Throws(new KeyNotFoundException());
            var controller = new PersonController(mock.Object);

            var result = controller.Put(id, person);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;
            var value = okResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(statusCode, 404);
        }
        #endregion

        #region Post()
        [TestMethod]
        public void PostCorrect()
        {
            int id = 1;
            Person person = new Person()
            {
                Name = "Nicolas",
                Surname = "Fierro",
                Phone = "+598123456",
                Email = "nico@nico.com"
            };
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.Create(person)).Returns(id);
            var controller = new PersonController(mock.Object);

            var result = controller.Post(person);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;
            var value = okResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(value, "Se creo la persona con id: " + id);
            Assert.AreEqual(statusCode, 200);
        }
        #endregion

        #region Delete()
        [TestMethod]
        public void DeleteCasetExist()
        {
            int id = 1;
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.Remove(id));
            var controller = new PersonController(mock.Object);

            var result = controller.Delete(id);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(statusCode, 200);
        }
        [TestMethod]
        public void DeleteCaseNotExist()
        {
            int id = 2;
            var mock = new Mock<IPersonService>(MockBehavior.Strict);

            mock.Setup(service => service.Remove(id)).Throws(new KeyNotFoundException());
            var controller = new PersonController(mock.Object);

            var result = controller.Delete(id);
            var okResult = result as ObjectResult;
            var statusCode = okResult.StatusCode;
            var value = okResult.Value;

            mock.VerifyAll();
            Assert.AreEqual(statusCode, 404);
        }
        #endregion
    }
}
