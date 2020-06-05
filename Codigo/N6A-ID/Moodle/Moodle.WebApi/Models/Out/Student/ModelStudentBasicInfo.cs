using Moodle.Domain;

namespace Moodle.WebApi
{
    public class ModelStudentBasicInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ModelStudentBasicInfo(Student student)
        {
            this.Id = student.Id;
            this.Name = student.Name +" ("+student.StudentNumber+")";
        }
    }
}