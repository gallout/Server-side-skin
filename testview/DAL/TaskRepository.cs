using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testview.Interfaces;
using testview.Models;

namespace testview.DAL
{
    public class TaskRepository : ITaskRepository, IDisposable
    {
       
        
            private SchoolContext context;

            public TaskRepository(SchoolContext context)
            {
                this.context = context;
            }


            public IEnumerable<Tasks> GetTasks()
            {
                return context.Tasks.ToList();
            }

            public Tasks GetTaskByID(int id)
            {
                return context.Tasks.Find(id);
            }

            public void InsertTask(Tasks task)
            {
                context.Tasks.Add(task);
            }

            public void DeleteTask(int ID)
            {
                Tasks task = context.Tasks.Find(ID);
                context.Tasks.Remove(task);
            }

           

            public void Save()
            {
                context.SaveChanges();
            }

            private bool disposed = false;

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                }
                this.disposed = true;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            
            }
       }
}