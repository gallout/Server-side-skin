using System;
using testview.DAL;
using testview.Interfaces;
using testview.Models;

namespace testview.DAL
{
    public class UnitOfWork: IUnitOfWork , IDisposable
    {

        private SchoolContext context = new SchoolContext();
       
        private TaskRepository taskRepository;
        private StudentRepository studentRepository;
        private GenericRepository<Enrollment> enrollmentRepository;
        private GenericRepository<Course> courseRepository;

        public IStudentRepository StudentRepository
        {
            get
            {

                if (this.studentRepository == null)
                {
                    this.studentRepository = new StudentRepository(context);
                }
                return studentRepository;
            }
        }

        public ITaskRepository TaskRepository
        {
            get
            {

                if (this.taskRepository == null)
                {
                    this.taskRepository = new TaskRepository(context);
                }
                return taskRepository;
            }
        }

        public GenericRepository<Enrollment> DepartmentRepository
        {
            get
            {

                if (this.enrollmentRepository == null)
                {
                    this.enrollmentRepository = new GenericRepository<Enrollment>(context);
                }
                return enrollmentRepository;
            }
        }

        public GenericRepository<Course> CourseRepository
        {
            get
            {

                if (this.courseRepository == null)
                {
                    this.courseRepository = new GenericRepository<Course>(context);
                }
                return courseRepository;
            }
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
