using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.ProgDec.BL.Models
{
    public class Student
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Student Id")]
        public string StudentId { get; set; }
        public string FullName { 
            get
            {
                return FirstName + " " + LastName;
            } 
        }
    }
}
