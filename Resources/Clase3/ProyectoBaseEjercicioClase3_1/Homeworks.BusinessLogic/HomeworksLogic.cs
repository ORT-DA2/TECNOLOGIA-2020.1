using System;
using System.Collections.Generic;

using Homeworks.Domain;
using Homeworks.DataAccess;

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