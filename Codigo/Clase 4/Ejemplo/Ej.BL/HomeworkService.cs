using System;
using System.Collections.Generic;
using Ej.BL.Interface;
using Ej.Domain;
using Ej.DA.Interface;
using System.Linq;

namespace Ej.BL
{
    public class HomeworkService : IHomeworkService
    {
        private IManagerDA<Homework> ManagerDA;
        public HomeworkService(IManagerDA<Homework> managerDA) {
            this.ManagerDA = managerDA;
        }
        public int Create(Homework homework)
        {
            ManagerDA.Add(homework);
            ManagerDA.Save();
            return homework.Id;
        }
        public IEnumerable<Homework> GetAll()
        {
            return ManagerDA.GetAll();
        }
        public Homework Get(int id)
        {
            return ManagerDA.Get(id);
        }
    }
}
