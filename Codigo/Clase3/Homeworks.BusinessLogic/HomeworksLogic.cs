using System;
using Homeworks.DataAccess;
using Homeworks.Domain;
using System.Collections.Generic;

namespace Homeworks.BusinessLogic
{
    public class HomeworksLogic
    {
        private HomeworksRepository homeworksRepository;

        public HomeworksLogic() {
            homeworksRepository = new HomeworksRepository();
        }

        public List<Homework> GetHomeworks() {
            return homeworksRepository.GetHomeworks();
        }
    }
}
