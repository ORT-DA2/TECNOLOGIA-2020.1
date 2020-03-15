using System;
using System.Collections.Generic;
using System.Linq;
using Homeworks.Domain;
using Microsoft.EntityFrameworkCore;

namespace Homeworks.DataAccess
{
    public class HomeworksRepository: IDisposable
    {
         public HomeworksRepository(DbContext context)
        {
            Context = context;
        }
        // 1 - Creacion 
        protected DbContext Context {get; set;}

       

        // 2- Acceso y manipulacion de datos en la DB

        public Homework Get(Guid id)
        {
            return Context.Set<Homework>().Include("Exercises").First(x => x.Id == id);
        }

        public IEnumerable<Homework> GetAll()
        {
            return Context.Set<Homework>().Include("Exercises").ToList();
        }

        public void Add(Homework entity) {
            Context.Set<Homework>().Add(entity);
        }

        public void Remove(Homework entity) {
            Context.Set<Homework>().Remove(entity);
        }

        public void Update(Homework entity) {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Save() {
            Context.SaveChanges();
        }

        // 3 - Disposing

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}