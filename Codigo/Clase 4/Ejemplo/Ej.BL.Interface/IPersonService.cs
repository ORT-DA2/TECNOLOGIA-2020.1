using System;
using Ej.Domain;
using System.Collections.Generic;

namespace Ej.BL.Interface
{
    public interface IPersonService
    {
        int Create(Person person);
        void Remove(int id);
        void Update(Person person);
        IEnumerable<Person> GetAll();
        Person Get(int id);
    }
}
