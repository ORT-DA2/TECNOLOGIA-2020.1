using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moodle.DataAccess.Interface;
using Moodle.Domain;

namespace Moodle.DataAccess
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly DbContext context;
        private readonly DbSet<Student> dbSetStudent;

        public StudentRepository(DbContext dbContext)
        {
            this.context = dbContext;
            this.dbSetStudent = dbContext.Set<Student>();
        }

        public Student Get(Func<Student, bool> predicate)
        {
            return this.context.Set<Student>().FirstOrDefault(predicate);
        }

        public IEnumerable<Student> GetAll()
        {
            return this.dbSetStudent.AsQueryable<Student>();
        }

        public IEnumerable<Student> GetAllByCondition(Func<Student, bool> predicate)
        {
            return this.dbSetStudent.AsQueryable<Student>().Where(predicate);
        }

        public void Add(Student student)
        {
            this.context.Set<Student>().Add(student);
        }

        public void Update(Student student)
        {
            this.context.Entry<Student>(student).State = EntityState.Modified;
        }

        public void Delete(Student student)
        {
            this.context.Remove(student);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public IEnumerable<Student> Query(string query)
        {
            throw new NotImplementedException();
        }

        public bool Exist(Func<Student, bool> predicate)
        {
            return this.dbSetStudent.Where(predicate).Any();
        }
    }
}