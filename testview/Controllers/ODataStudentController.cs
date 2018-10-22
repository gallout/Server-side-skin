using Microsoft.AspNet.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using testview.Interfaces;
using testview.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace testview.Controllers
{
    public class ODataStudentController : ODataController
    {
        
        private readonly IUnitOfWork unitOfWork;

        public ODataStudentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        [EnableQuery]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var students = unitOfWork.StudentRepository.GetStudents();
            return Ok(students);
        }

        // GET api/<controller>
        [HttpGet]
        [EnableQuery]
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> GetStudent(int id)
        {   
            Student student = unitOfWork.StudentRepository.GetStudentByID(id);
            if (student == null)
            {
                return  NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        [EnableQuery]
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
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }



        [HttpPut]
        [EnableQuery]
        [ResponseType(typeof(void))]

        public HttpResponseMessage PutStudent(Student s, int id)
        {
            Student stud = unitOfWork.StudentRepository.GetStudentByID(id);

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
        [EnableQuery]
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

        [EnableQuery]
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
