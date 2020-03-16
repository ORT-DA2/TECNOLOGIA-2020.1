using System;
using System.Collections;
using System.Collections.Generic;
using Homeworks.Domain;

namespace Homeworks.DataAccess
{
    public class HomeworksRepository
    {
        public List<Homework> GetHomeworks() {
            List<Homework> homeworks = new List<Homework>();
            
            Exercise firstExercise = CreateExercise("firstProblem", 5);
            Exercise secondExercise = CreateExercise("secondProblem", 6);

            Homework firstHomework = CreateHomework("firstDescription", firstExercise);
            homeworks.Add(firstHomework);

            Homework secondHomework = CreateHomework("secondDescription", secondExercise);
            homeworks.Add(secondHomework);

            return homeworks;
        }

        private Exercise CreateExercise(String problem, int score) {
            Exercise exercise = new Exercise();
            exercise.Problem = problem;
            exercise.Score = score;
            return exercise;
        }

        private Homework CreateHomework(String description, Exercise exercise) {
            Homework homework = new Homework();
            homework.Exercises.Add(exercise);
            homework.DueDate = DateTime.Now;
            homework.Description = description;
            return homework;
        }
    }
}