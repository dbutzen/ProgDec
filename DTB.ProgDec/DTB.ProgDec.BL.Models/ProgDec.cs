using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.ProgDec.BL.Models
{
    public class ProgDec
    {
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public int StudentId { get; set; }
        [DisplayName("Change Date")]
        public DateTime ChangeDate { get; set; }
        [DisplayName("Program Name")]
        public string ProgramName { get; set; }
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [DisplayName("Degree Type Name")]
        public string DegreeTypeName { get; set; }
        public List<Advisor> Advisors { get; set; }

        public ProgDec()
        {
            Advisors = new List<Advisor>();
        }
    }
}
