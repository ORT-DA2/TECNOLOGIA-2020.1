using System;
using System.Collections.Generic;
using Moodle.BusinessLogic.Interface;
using Moodle.DataAccess;
using Moodle.DataAccess.Interface;
using Moodle.Domain;

namespace Moodle.BusinessLogic
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IRepository<Student> studentRepository;

        public StudentLogic(IRepository<Student> _studentRepository)
        {
            this.studentRepository = _studentRepository;
        }

        public IEnumerable<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetAllByCondition(Func<Student, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public Student Add(Student newStudent)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Student newStudent)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}