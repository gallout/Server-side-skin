using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testview.Models;

namespace testview.Interfaces
{
    public interface ITaskRepository : IDisposable
    {
        IEnumerable<Tasks> GetTasks();
        Tasks GetTaskByID(int ID);
        void InsertTask(Tasks task);
        void DeleteTask(int ID);
        void Save();
   }
}