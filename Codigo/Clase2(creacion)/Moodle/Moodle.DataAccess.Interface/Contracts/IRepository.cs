using System;
using System.Collections.Generic;

namespace Moodle.DataAccess.Interface
{
    public interface IRepository<T> where T : class
    {
        T Get(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllByCondition(Func<T, bool> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Exist(Func<T, bool> predicate);
        IEnumerable<T> Query(string query);
        void SaveChanges();
    }
}