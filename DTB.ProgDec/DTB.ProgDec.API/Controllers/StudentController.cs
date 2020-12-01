using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DTB.ProgDec.BL;
using DTB.ProgDec.BL.Models;

namespace DTB.ProgDec.API.Controllers
{
    public class StudentController : ApiController
    {
        // GET: api/Student
        public IEnumerable<Student> Get()
        {
            List<Student> students = StudentManager.Load();
            return students;
        }

        // GET: api/Student/5
        public Student Get(int id)
        {
            Student student = StudentManager.LoadById(id);
            return student;
        }

        // POST: api/Student
        public void Post([FromBody] Student student)
        {
            StudentManager.Insert(student);
        }

        // PUT: api/Student/5
        public void Put(int id, [FromBody] Student student)
        {
            StudentManager.Update(student);
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {
            StudentManager.Delete(id);
        }
    }
}
