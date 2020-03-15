using System;
using System.Collections.Generic;

using Homeworks.Domain;
using Homeworks.DataAccess;

namespace Homeworks.BusinessLogic
{
    public class HomeworksLogic: IDisposable
    {
        private HomeworksRepository homeworksRepository;

        public HomeworksLogic() {
            HomeworksContext context = ContextFactory.GetNewContext();
            homeworksRepository = new HomeworksRepository(context);
        }
 
        public Homework Create(Homework homework) {
            //1
            homeworksRepository.Add(homework);
            //nunca olvidar esto !
            homeworksRepository.Save();
            return homework;
        }
              public void Remove(Guid id) {
            //2
            Homework homework = homeworksRepository.Get(id);
            if (homework == null) {
                //TODO: ¿Se pueden manejar mejor los mensaje de las excepciones?
                throw new ArgumentException("Invalid guid");
            }
            homeworksRepository.Remove(homework);
            //nunca olvidar esto !
            homeworksRepository.Save();
        }

        public Exercise AddExercise(Guid id, Exercise exercise)
        {
            Homework homework = homeworksRepository.Get(id);
            if (homework == null) {
                //TODO: ¿Se pueden manejar mejor los mensaje de las excepciones?
                throw new ArgumentException("Invalid guid");
            }
            homework.Exercises.Add(exercise);
            homeworksRepository.Update(homework);
            //nunca olvidar esto !
            homeworksRepository.Save();
            return exercise;
        }

  

        public Homework Update(Guid id, Homework homework) {
            //3
            Homework homeworkToUpdate = homeworksRepository.Get(id);
            if (homeworkToUpdate == null) {
                //TODO: ¿Se pueden manejar mejor los mensaje de las excepciones?
                throw new ArgumentException("Invalid guid");
            }
            homeworkToUpdate.Description = homework.Description;
            homeworkToUpdate.DueDate = homework.DueDate;
            homeworksRepository.Update(homeworkToUpdate);
            //nunca olvidar esto !
            homeworksRepository.Save();
            return homeworkToUpdate;
        }

        public Homework Get(Guid id) {
            //4
            return homeworksRepository.Get(id);
        }

        public IEnumerable<Homework> GetHomeworks() {
            return homeworksRepository.GetAll();
        }

    public void Dispose()
    {
        //TODO: ¿Por qué implementamos esto? ¿Cuándo se usa?
      homeworksRepository.Dispose();
    }
  }
}