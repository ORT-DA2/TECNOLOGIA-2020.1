using System;
using System.Collections.Generic;
using Ej.BL.Interface;
using Ej.Domain;
using Ej.DA.Interface;
using System.Linq;

namespace Ej.BL
{
    public class PersonService : IPersonService
    {
        private IManagerDA<Person> ManagerDA;
        public PersonService(IManagerDA<Person> managerDA) {
            this.ManagerDA = managerDA;
        }
        public int Create(Person person)
        {
            if(ExistPerson(person)) throw new Exception("La persona ya existe");
            ManagerDA.Add(person);
            ManagerDA.Save();
            return person.Id;
        }
        private bool ExistPerson(Person person)
        {
            return ManagerDA.GetAll().Any(x => x.Email == person.Email || x.Phone == person.Phone);
        }
        public void Remove(int id)
        {
            ManagerDA.Remove(id);
            ManagerDA.Save();
        }
        public void Update(Person person)
        {
            ManagerDA.Update(person);
            ManagerDA.Save();
        }
        public IEnumerable<Person> GetAll()
        {
            return ManagerDA.GetAll();
        }
        public Person Get(int id)
        {
            return ManagerDA.Get(id);
        }
    }
}
