using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.ProgDec.BL.Models
{
    public class Program
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int DegreeTypeId { get; set; }
        [DisplayName("Degree Name")]
        public string DegreeName { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }

    }
}
