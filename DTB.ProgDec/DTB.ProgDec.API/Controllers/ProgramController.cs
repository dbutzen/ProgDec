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
    public class ProgramController : ApiController
    {
        // GET: api/Program
        public IEnumerable<Program> Get()
        {
            List<Program> programs = ProgramManager.Load();
            return programs;
        }

        // GET: api/Program/5
        public Program Get(int id)
        {
            Program program = ProgramManager.LoadById(id);
            return program;
        }

        // POST: api/Program
        public void Post([FromBody]Program program)
        {
            ProgramManager.Insert(program);
        }

        // PUT: api/Program/5
        public void Put(int id, [FromBody]Program program)
        {
            ProgramManager.Update(program);
        }

        // DELETE: api/Program/5
        public void Delete(int id)
        {
            ProgramManager.Delete(id);
        }
    }
}
