using System;
using System.Collections.Generic;
using Moodle.Domain;

namespace Moodle.BusinessLogic.Interface
{
    public interface ILogic <T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllByCondition(Func<Student, bool> predicate);
        T Add(T newEntity);
        void Update(int id, T newEntity);
        void Delete(int id);
    }
}