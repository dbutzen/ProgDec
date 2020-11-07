using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.BL;

namespace DTB.ProgDec.MVCUI.ViewModels
{
    public class ProgramDegreeTypes
    {
        public Program Program { get; set; }
        public List<DegreeType> DegreeTypes { get; set; }
        public HttpPostedFileBase File { get; set; }

    }
}