using System;
using System.Collections.Generic;
using Ej.DA.Interface;
using Ej.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ej.DA
{
    public class PersonManagerDA : IManagerDA<Person>
    {
        protected DbContext Context { get; set; }
        public PersonManagerDA(DbContext context)
        {
            Context = context;
        }
        public void Add(Person person)
        {
            Context.Set<Person>().Add(person);
        }

        public void Remove(int id)
        {
            Person person = Get(id);
            if (person == null) throw new KeyNotFoundException("La persona no existe");
            else Context.Set<Person>().Remove(person);
        }

        public void Update(Person person)
        {
            bool exist = Context.Set<Person>().Any(p => p.Id == person.Id);
            if (exist) Context.Set<Person>().Update(person);
            else throw new KeyNotFoundException("La persona no existe");
        }

        public IEnumerable<Person> GetAll()
        {
            return Context.Set<Person>();
        }

        public Person Get(int id)
        {
            return Context.Set<Person>().FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
