using System;
using Moodle.BusinessLogic.Interface;
using Moodle.DataAccess.Interface;
using Moodle.Domain;

namespace Moodle.BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IRepository<Student> studentRepository;
        
        public SessionLogic(IRepository<Student> _studentRepository)
        {
            this.studentRepository = _studentRepository;
        }

        public Guid Login(string credential, string password)
        {
            try
            {
                var student = this.studentRepository.Get(s => s.StudentNumber == credential && s.Password == password);

                if(student.Token == Guid.Empty)
                {
                    student.Token = Guid.NewGuid();

                    this.studentRepository.Update(student);
                    this.studentRepository.SaveChanges();
                }

                return student.Token;
            }
            catch(Exception)
            {
                throw new Exception("Invalid credentials");
            }
        }

        public bool IsValidToken(Guid token)
        {
            return this.studentRepository.Exist(s => s.Token == token);
        }
    }
}