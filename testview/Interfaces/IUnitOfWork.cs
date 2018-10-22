using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using testview.DAL;
using testview.Models;

namespace testview.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
         IStudentRepository StudentRepository { get; }
        ITaskRepository TaskRepository { get; }
        void Save();
    }
}
