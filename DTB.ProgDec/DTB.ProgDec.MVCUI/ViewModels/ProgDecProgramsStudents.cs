using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTB.ProgDec.BL.Models;
using DTB.ProgDec.BL;

namespace DTB.ProgDec.MVCUI.ViewModels
{
    public class ProgDecProgramsStudents
    {
        public BL.Models.ProgDec ProgDec { get; set; }
        public List<BL.Models.Program> Programs { get; set; }
        public List<BL.Models.Student> Students { get; set; }
    }
}