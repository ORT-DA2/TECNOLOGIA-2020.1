using System;
using Ej.Domain;
using System.Collections.Generic;

namespace Ej.BL.Interface
{
    public interface IHomeworkService
    {
        int Create(Homework homework);
        IEnumerable<Homework> GetAll();
        Homework Get(int id);
    }
}
