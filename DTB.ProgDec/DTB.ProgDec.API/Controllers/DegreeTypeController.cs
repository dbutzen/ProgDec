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
    public class DegreeTypeController : ApiController
    {
        // GET: api/DegreeType
        public IEnumerable<DegreeType> Get()
        {
            List<DegreeType> degreeTypes = DegreeTypeManager.Load();
            return degreeTypes;
        }

        // GET: api/DegreeType/5
        public DegreeType Get(int id)
        {
            DegreeType degreeType = DegreeTypeManager.LoadById(id);
            return degreeType;
        }

        // POST: api/DegreeType
        public void Post([FromBody] DegreeType degreeType)
        {
            DegreeTypeManager.Insert(degreeType);
        }

        // PUT: api/DegreeType/5
        public void Put(int id, [FromBody] DegreeType degreeType)
        {
            DegreeTypeManager.Update(degreeType);
        }

        // DELETE: api/DegreeType/5
        public void Delete(int id)
        {
            DegreeTypeManager.Delete(id);
        }
    }
}
