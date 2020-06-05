using Moodle.Domain;

namespace Moodle.WebApi
{
    public class ModelCourseBasicInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ModelCourseBasicInfo(Course course)
        {
            this.Id = course.Id;
            this.Name = course.Name;
        }
    }
}