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

namespace testview.Controllers
{
    public class TaskController : ApiController
    {

        private readonly IUnitOfWork unitOfWork;

        public TaskController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult GetTasks()
        {
            var tasks = unitOfWork.TaskRepository.GetTasks();
            return Ok(tasks);
        }

        // GET api/<controller>
        [HttpGet]
        [ResponseType(typeof(Tasks))]
        public async Task<IHttpActionResult> GetTask(int id)
        {
            Tasks task = unitOfWork.TaskRepository.GetTaskByID(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [ResponseType(typeof(Tasks))]
        public async Task<IHttpActionResult> PostTask(Tasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                unitOfWork.TaskRepository.InsertTask(task);
                unitOfWork.TaskRepository.Save();

                return Ok(task);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [HttpDelete]
        [ResponseType(typeof(Tasks))]
        public async Task<IHttpActionResult> DeleteTask(int id)
        {
            Tasks task = unitOfWork.TaskRepository.GetTaskByID(id);
            if (task == null)
            {
                return NotFound();
            }

            unitOfWork.TaskRepository.DeleteTask(id);
            unitOfWork.TaskRepository.Save();

            return Ok(task);
        }
    



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.TaskRepository.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
