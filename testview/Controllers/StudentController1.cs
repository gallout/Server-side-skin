using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using testview.DAL;
using System.Web.Http.Results;
using testview.Models;
using System.Web.Http.Description;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using testview.Migrations;
using System.Threading;
using testview.Interfaces;

namespace testview.Controllers
{
    public class StudentController : ApiController
    {

        private readonly IUnitOfWork unitOfWork;

         public StudentController(IUnitOfWork unitOfWork)
         {
            this.unitOfWork = unitOfWork;
         } 

        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var students = unitOfWork.StudentRepository.GetStudents();
            return Ok(students);
        }

        // GET api/<controller>
        [HttpGet]
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> GetStudent(int id)
        {
            Student student = unitOfWork.StudentRepository.GetStudentByID(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                unitOfWork.StudentRepository.InsertStudent(student);
                unitOfWork.StudentRepository.Save();

                return Ok(student);
            } catch (Exception e)
            {
                return InternalServerError(e);
            }
        }



        [HttpPut]
        [ResponseType(typeof(void))]
        
        public HttpResponseMessage PutStudent(Student s, int id)
        {
            Student stud= unitOfWork.StudentRepository.GetStudentByID(id);

            if (stud == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            // stud.ID = s.ID;
            //  stud.LastName = s.LastName;
            //   stud.FirstMidName = s.FirstMidName;
            unitOfWork.StudentRepository.UpdateStudent(stud);
            unitOfWork.StudentRepository.Save();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        [HttpDelete]
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> DeleteStudent(int id)
        {
            Student student = unitOfWork.StudentRepository.GetStudentByID(id);
            if (student == null)
            {
                return NotFound();
            }

            unitOfWork.StudentRepository.DeleteStudent(id);
            unitOfWork.StudentRepository.Save();

            return Ok(student);
        }
        /*
        [HttpDelete]
        [ResponseType(typeof(Student))]
        public async Task <IHttpActionResult> DeleteStudent([FromUri] string lastName)
        {
            Student student = await db.Students.FirstOrDefaultAsync(x => x.LastName == lastName);

            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            await db.SaveChangesAsync();

            return Ok(student);
        }


        [HttpDelete]
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> DeleteStudents([FromUri] string lastName)
        {
            List<Student> studentList = await db.Students.Where(x => x.LastName == lastName).ToListAsync();

            if (studentList == null)
            {
                return NotFound();
            }

            
            foreach(var student in studentList) {
                db.Students.Remove(student);
            }
          
            await db.SaveChangesAsync();
            return Ok();
        }

        */



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.StudentRepository.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
