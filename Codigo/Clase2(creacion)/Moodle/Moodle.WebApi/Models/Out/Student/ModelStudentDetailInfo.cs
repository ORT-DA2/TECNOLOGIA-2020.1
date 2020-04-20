using System.Collections.Generic;
using System.Linq;
using Moodle.Domain;

namespace Moodle.WebApi
{
    public class ModelStudentDetailInfo
    {
        public int Id { get; }
        public string Name { get; }
        public string StudentNumber { get; }
        public List<ModelCourseBasicInfo> Courses { get; }

        public ModelStudentDetailInfo(Student student)
        {
            this.Id = student.Id;
            this.Name = student.Name;
            this.StudentNumber = student.StudentNumber;
            this.Courses = student.Courses.Select(sc => new ModelCourseBasicInfo(sc.Course)).ToList();
        }
    }
}