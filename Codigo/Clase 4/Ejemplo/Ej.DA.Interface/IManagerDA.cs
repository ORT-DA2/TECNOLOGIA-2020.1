using System;
using System.Collections.Generic;

namespace Ej.DA.Interface
{
    public interface IManagerDA<T>
    {
        void Add(T entity);

        void Remove(int id);

        void Update(T entity);

        IEnumerable<T> GetAll();

        T Get(int id);

        void Save();
    }
}
