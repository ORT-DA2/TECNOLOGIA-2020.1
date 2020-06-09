using System;
using System.Collections.Generic;
using Ej.DA.Interface;
using Ej.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Ej.DA
{
    public class HomeworkManagerDA : IManagerDA<Homework>
    {
        protected DbContext Context { get; set; }
        public HomeworkManagerDA(DbContext context)
        {
            Context = context;
        }
        public void Add(Homework homework)
        {
            Context.Set<Homework>().Add(homework);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Homework homework)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Homework> GetAll()
        {
            return Context.Set<Homework>();
        }

        public Homework Get(int id)
        {
            return Context.Set<Homework>().FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
