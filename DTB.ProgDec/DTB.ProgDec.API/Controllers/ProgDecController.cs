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
    public class ProgDecController : ApiController
    {
        // GET: api/ProgDec
        public IEnumerable<BL.Models.ProgDec> Get()
        {
            List<BL.Models.ProgDec> progDecs = ProgDecManager.Load();
            return progDecs;
        }

        // GET: api/ProgDec/5
        public BL.Models.ProgDec Get(int id)
        {
            BL.Models.ProgDec progDec = ProgDecManager.LoadById(id);
            return progDec;
        }

        // POST: api/ProgDec
        public void Post([FromBody] BL.Models.ProgDec progDec)
        {
            ProgDecManager.Insert(progDec);
        }

        // PUT: api/ProgDec/5
        public void Put(int id, [FromBody] BL.Models.ProgDec progDec)
        {
            ProgDecManager.Update(progDec);
        }

        // DELETE: api/ProgDec/5
        public void Delete(int id)
        {
            ProgDecManager.Delete(id);
        }
    }
}
